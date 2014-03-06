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
    public class ApplicationController : ApiControllerBase
    {
        readonly IQueryHandler<FindAll<Application>, IEnumerable<Application>> _allApplications;
        readonly IQueryHandler<FindById<Application>, Application> _getApplication;
        readonly ICommandHandler<SaveAggregateRoot<Application>> _saveApplication;
        readonly ICommandHandler<UpdateAggregateRoot<Application>> _updateApplication;
        readonly ICommandHandler<DeleteAggregateRoot<Application>> _deleteApplication;

        public ApplicationController(
            IQueryHandler<FindAll<Application>, IEnumerable<Application>> allApplications,
            IQueryHandler<FindById<Application>, Application> getApplication,
            ICommandHandler<SaveAggregateRoot<Application>> saveApplication,
            ICommandHandler<UpdateAggregateRoot<Application>> updateApplication,
            ICommandHandler<DeleteAggregateRoot<Application>> deleteApplication)
        {
            _allApplications = allApplications;
            _saveApplication = saveApplication;
            _getApplication = getApplication;
            _deleteApplication = deleteApplication;
            _updateApplication = updateApplication;
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            return Get(() => _allApplications.Handle(new FindAll<Application>()).OrderBy(x => x.Name));
        }

        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            return Get(() => _getApplication.Handle(new FindById<Application>(id)));
        }

        [HttpPost]
        public HttpResponseMessage Create(Application app)
        {
            if (app == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Application is NULL");
            }

            return Post(() => _saveApplication.Handle(new SaveAggregateRoot<Application>(app)));
        }

        [HttpPost]
        public HttpResponseMessage Delete(string id)
        {
            return Delete(() => _deleteApplication.Handle(new DeleteAggregateRoot<Application>(id)));
        }

        [HttpPost]
        public HttpResponseMessage Update(Application app)
        {
            if (app == null || string.IsNullOrEmpty(app.Id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Application is NULL");
            }

            var updated = _getApplication.Handle(new FindById<Application>(app.Id));
            if (updated == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Application Id not found");
            }

            updated.Version = updated.Version ?? new Version();
            updated.Name = app.Name;
            updated.Owner = app.Owner;
            updated.AccessKey = app.AccessKey;
            updated.Version.LastUpdatedBy = "mauri";
            updated.Version.LastUpdatedOn = DateTime.Now;

            return Post(() => _updateApplication.Handle(new UpdateAggregateRoot<Application>(updated)));
        }
    }
}
