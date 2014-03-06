using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Commands;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Version = Reconfig.Domain.Model.Version;

namespace Reconfig.Web.Controllers.Api
{
    public class GlobalSettingController : ApiControllerBase
    {
        readonly IQueryHandler<FindAll<GlobalSetting>, IEnumerable<GlobalSetting>> _all;
        readonly IQueryHandler<FindById<GlobalSetting>, GlobalSetting> _get;
        readonly ICommandHandler<SaveAggregateRoot<GlobalSetting>> _save;
        readonly ICommandHandler<UpdateAggregateRoot<GlobalSetting>> _update;
        readonly ICommandHandler<DeleteAggregateRoot<GlobalSetting>> _delete;

        public GlobalSettingController(IQueryHandler<FindAll<GlobalSetting>, IEnumerable<GlobalSetting>> all,
            IQueryHandler<FindById<GlobalSetting>, GlobalSetting> get,
            ICommandHandler<SaveAggregateRoot<GlobalSetting>> save,
            ICommandHandler<UpdateAggregateRoot<GlobalSetting>> update,
            ICommandHandler<DeleteAggregateRoot<GlobalSetting>> delete)
        {
            _all = all;
            _save = save;
            _get = get;
            _delete = delete;
            _update = update;
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            return Get(() => _all.Handle(new FindAll<GlobalSetting>()).OrderBy(x => x.SectionName).ThenBy(x => x.Key));
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            return Get(() => _get.Handle(new FindById<GlobalSetting>(id)));
        }

        [HttpPost]
        public HttpResponseMessage Create(GlobalSetting app)
        {
            if (app == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "GlobalSetting is NULL");
            }

            return Post(() => _save.Handle(new SaveAggregateRoot<GlobalSetting>(app)));
        }

        [HttpPost]
        public HttpResponseMessage Delete(string id)
        {
            return Delete(() => _delete.Handle(new DeleteAggregateRoot<GlobalSetting>(id)));
        }

        [HttpPost]
        public HttpResponseMessage Update(GlobalSetting setting)
        {
            if (setting == null || string.IsNullOrEmpty(setting.Id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "GlobalSetting is NULL");
            }

            var updated = _get.Handle(new FindById<GlobalSetting>(setting.Id));
            if (updated == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "GlobalSetting Id not found");
            }

            updated.Version = updated.Version ?? new Version();
            updated.SectionName = setting.SectionName;
            updated.Key = setting.Key;
            updated.Value = setting.Value;
            updated.Environment = setting.Environment;
            updated.Version.LastUpdatedBy = "mauri";
            updated.Version.LastUpdatedOn = DateTime.Now;

            return Put(() => _update.Handle(new UpdateAggregateRoot<GlobalSetting>(updated)));
        }
    }
}
