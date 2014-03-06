using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Commands;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;

namespace Reconfig.Web.Controllers.Api
{
    public class SectionController : ApiControllerBase
    {
        readonly IQueryHandler<FindById<Configuration>, Configuration> _findById;
        readonly ICommandHandler<UpdateAggregateRoot<Configuration>> _update;

        public SectionController(
            IQueryHandler<FindById<Configuration>, Configuration> findById,
            ICommandHandler<UpdateAggregateRoot<Configuration>> update)
        {
            _update = update;
            _findById = findById;
        }

        [HttpPost]
        public HttpResponseMessage Add(string configId, ConfigurationSection section)
        {
            return Update(configId, section);
        }

        [HttpPost]
        public HttpResponseMessage Update(string configId, ConfigurationSection section)
        {
            var config = _findById.Handle(new FindById<Configuration>(configId));
            if (config == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return Post(() =>
            {
                if (section != null && section.Name != null)
                {
                    var sec = config.Sections.FirstOrDefault(x => x.Name == section.Name);
                    if (sec == null)
                    {
                        config.Sections.Add(section);
                    }
                    else
                    {
                        sec.Settings = section.Settings;
                    }
                    _update.Handle(new UpdateAggregateRoot<Configuration>(config));
                }
            });
        }

        [HttpGet]
        public HttpResponseMessage Get(string configId, string sectionName)
        {
            var config = _findById.Handle(new FindById<Configuration>(configId));
            if (config == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return Get(() => config.Sections.FirstOrDefault(x => x.Name == sectionName));
        }

        [HttpPost]
        public HttpResponseMessage Delete(string configId, string sectionName)
        {
            var config = _findById.Handle(new FindById<Configuration>(configId));
            if (config == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return Post(() =>
            {
                if (sectionName != null)
                {
                    var index = config.Sections.FindIndex(x => x.Name == sectionName);
                    config.Sections.RemoveAt(index);
                }
                _update.Handle(new UpdateAggregateRoot<Configuration>(config));
            });
        }
    }
}