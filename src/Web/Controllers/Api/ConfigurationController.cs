using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Commands;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;

namespace Reconfig.Web.Controllers.Api
{
    public class ConfigurationController : ApiControllerBase
    {
        readonly IQueryHandler<FindById<Configuration>, Configuration> _findById;
        readonly IQueryHandler<FindConfigurationByUrl, Configuration> _findByUrl;
        readonly IQueryHandler<FindConfigurationByName, Configuration> _findByName;
        readonly IQueryHandler<FindByApplicationId<Configuration>, IEnumerable<Configuration>> _findByAppId;
        readonly ICommandHandler<SaveAggregateRoot<Configuration>> _save;
        readonly ICommandHandler<UpdateAggregateRoot<Configuration>> _update;
        readonly ICommandHandler<DeleteAggregateRoot<Configuration>> _delete;

        public ConfigurationController(
            IQueryHandler<FindApplicationByName, Application> findAppByName,
            IQueryHandler<FindConfigurationByUrl, Configuration> findByUrl,
            IQueryHandler<FindConfigurationByName, Configuration> findByName,
            IQueryHandler<FindById<Configuration>, Configuration> findById,
            IQueryHandler<FindByApplicationId<Configuration>, IEnumerable<Configuration>> findByAppId,
            ICommandHandler<SaveAggregateRoot<Configuration>> save,
            ICommandHandler<UpdateAggregateRoot<Configuration>> update,
            ICommandHandler<DeleteAggregateRoot<Configuration>> delete)
            : base(findAppByName)
        {
            _save = save;
            _update = update;
            _findByUrl = findByUrl;
            _findById = findById;
            _findByName = findByName;
            _findByAppId = findByAppId;
            _delete = delete;
        }

        [HttpPost]
        public HttpResponseMessage Create(Configuration configuration)
        {
            if (configuration == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "configuration is NULL");
            }

            return Post(() => _save.Handle(new SaveAggregateRoot<Configuration>(configuration)));
        }

        [HttpPost]
        public HttpResponseMessage Update(Configuration configuration)
        {
            if (configuration == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "configuration is NULL");
            }

            var config = _findById.Handle(new FindById<Configuration>(configuration.Id));
            if (config == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            config.Name = configuration.Name;
            config.Environment = configuration.Environment;
            config.ParentId = configuration.ParentId;
            config.Sections = configuration.Sections;
            config.Url = configuration.Url;

            return Post(() => _update.Handle(new UpdateAggregateRoot<Configuration>(config)));
        }

        [HttpPost]
        public HttpResponseMessage Delete(string id)
        {
            return Delete(() => _delete.Handle(new DeleteAggregateRoot<Configuration>(id)));
        }

        [HttpPost]
        public HttpResponseMessage Clone(string id)
        {
            var existing = _findById.Handle(new FindById<Configuration>(id));
            if (existing != null)
            {
                existing.Id = null;
                existing.Name = string.Format("Clone of {0}", existing.Name);
                return Post(() => _save.Handle(new SaveAggregateRoot<Configuration>(existing)));
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            return Get(() => _findById.Handle(new FindById<Configuration>(id)));
        }

        [HttpGet]
        public HttpResponseMessage ByApp(string id)
        {
            return Get(() => _findByAppId.Handle(new FindByApplicationId<Configuration>(id)));
        }

        [HttpGet]
        public HttpResponseMessage ByUrl(string url)
        {
            return Get(() => _findByUrl.Handle(new FindConfigurationByUrl(url)));
        }

        [HttpGet]
        public HttpResponseMessage ByName(string name, string appName)
        {
            var app = FindApplication(appName);
            if (app == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return Get(() => _findByName.Handle(new FindConfigurationByName(name, app.Id)));
        }
    }
}