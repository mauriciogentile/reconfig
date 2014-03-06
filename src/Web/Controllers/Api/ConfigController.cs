using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;

namespace Reconfig.Web.Controllers.Api
{
    [AllowAnonymous]
    public class ConfigController : ApiControllerBase
    {
        readonly IQueryHandler<FindById<Configuration>, Configuration> _findById;
        readonly IQueryHandler<FindConfigurationByUrl, Configuration> _findByUrl;
        readonly IQueryHandler<FindConfigurationByName, Configuration> _findByName;
        readonly IQueryHandler<FindAll<GlobalSetting>, IEnumerable<GlobalSetting>> _allSettings;

        public ConfigController(
            IQueryHandler<FindApplicationByName, Application> findAppByName,
            IQueryHandler<FindConfigurationByUrl, Configuration> findByUrl,
            IQueryHandler<FindConfigurationByName, Configuration> findByName,
            IQueryHandler<FindById<Configuration>, Configuration> findById,
            IQueryHandler<FindAll<GlobalSetting>, IEnumerable<GlobalSetting>> allSettings)
            : base(findAppByName)
        {
            _findByUrl = findByUrl;
            _findById = findById;
            _findByName = findByName;
            _allSettings = allSettings;
        }

        [HttpGet]
        public HttpResponseMessage Resolve(string name, string appName)
        {
            var app = FindApplication(appName);
            if (app == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return Get(() =>
            {
                var configuration = _findByName.Handle(new FindConfigurationByName(name, app.Id));
                if (configuration != null)
                {
                    MergeSettings(configuration);
                }
                return configuration;
            });
        }

        [HttpGet]
        public HttpResponseMessage Resolve(string url)
        {
            return Get(() =>
            {
                var configuration = _findByUrl.Handle(new FindConfigurationByUrl(url));
                if (configuration != null)
                {
                    MergeSettings(configuration);
                }
                return configuration;
            });
        }

        void MergeSettings(Configuration configuration)
        {
            var mergedSections = new Dictionary<string, KeyValuePair<string, ConfigurationEntry>>();
            var globalSettings = _allSettings.Handle(new FindAll<GlobalSetting>())
                .Where(x => x.Environment == configuration.Environment || x.Environment == Environment.Any)
                .ToList();

            globalSettings.ForEach(x =>
            {
                var key = string.Format("{0}_{1}", x.SectionName, x.Key);
                mergedSections.Add(key, new KeyValuePair<string, ConfigurationEntry>(x.Key, new ConfigurationEntry
                {
                    Key = x.Key,
                    Value = x.Value
                }));
            });

            Merge(configuration, ref mergedSections);
            configuration.Sections = ToSections(mergedSections);
        }

        void Merge(Configuration conf, ref Dictionary<string, KeyValuePair<string, ConfigurationEntry>> allSections)
        {
            if (conf.ParentId != null)
            {
                Configuration parent = _findById.Handle(new FindById<Configuration>(conf.ParentId));
                Merge(parent, ref allSections);
            }
            MergeSections(conf.Sections, ref allSections);
        }

        List<ConfigurationSection> ToSections(Dictionary<string, KeyValuePair<string, ConfigurationEntry>> allSections)
        {
            var groups = allSections.Select(x => x.Value).GroupBy(x => x.Key);
            var sections = new List<ConfigurationSection>();
            foreach (var keyValuePair in groups)
            {
                sections.Add(new ConfigurationSection
                {
                    Name = keyValuePair.Key,
                    Settings = keyValuePair.Select(x => x.Value)
                });
            }
            return sections;
        }

        void MergeSections(IEnumerable<ConfigurationSection> sections, ref Dictionary<string, KeyValuePair<string, ConfigurationEntry>> allSections)
        {
            foreach (var section in sections)
            {
                foreach (var setting in section.Settings)
                {
                    var key = string.Format("{0}_{1}", section.Name, setting.Key);
                    var value = new KeyValuePair<string, ConfigurationEntry>(section.Name, setting);

                    if (!allSections.ContainsKey(key))
                    {
                        allSections.Add(key, value);
                    }
                    else
                    {
                        allSections[key] = value;
                    }
                }
            }
        }
    }
}