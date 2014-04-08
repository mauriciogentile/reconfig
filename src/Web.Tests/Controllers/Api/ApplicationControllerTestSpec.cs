using Moq;
using NUnit.Framework;
using Reconfig.Common.CQRS;
using Reconfig.Domain.Handlers.Queries;
using Reconfig.Domain.Model;
using Reconfig.Domain.Queries;
using Reconfig.Domain.Commands;
using Reconfig.Web.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Should;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Reconfig.Web.Tests.Controllers.Api
{
    [TestFixture]
    public class ApplicationControllerTestSpec
    {
        private Mock<IQueryHandler<FindAll<Application>, IEnumerable<Application>>> _allAppsQuery;
        private Mock<IQueryHandler<FindById<Application>, Application>> _appByIdQuery;
        private Mock<ICommandHandler<SaveAggregateRoot<Application>>> _saveAppCommand;
        private Mock<ICommandHandler<UpdateAggregateRoot<Application>>> _updateAppCommand;
        private Mock<ICommandHandler<DeleteAggregateRoot<Application>>> _deleteAppCommand;
        private HttpRequestMessage _request;

        [SetUp]
        public void Setup()
        {
            _allAppsQuery = new Mock<IQueryHandler<FindAll<Application>, IEnumerable<Application>>>();
            _appByIdQuery = new Mock<IQueryHandler<FindById<Application>, Application>>();

            _saveAppCommand = new Mock<ICommandHandler<SaveAggregateRoot<Application>>>();
            _saveAppCommand.Setup(x => x.Handle(It.IsAny<SaveAggregateRoot<Application>>()));

            _updateAppCommand = new Mock<ICommandHandler<UpdateAggregateRoot<Application>>>();
            _deleteAppCommand = new Mock<ICommandHandler<DeleteAggregateRoot<Application>>>();

            _request = new HttpRequestMessage();
            _request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }

        [Test]
        public void Should_Return_201_When_Creating_Application()
        {
            ApplicationController target = new ApplicationController(_allAppsQuery.Object, _appByIdQuery.Object,
                _saveAppCommand.Object, _updateAppCommand.Object, _deleteAppCommand.Object);

            target.Request = _request;

            target.Create(new Application()).StatusCode.ShouldEqual(HttpStatusCode.Created);
        }

        [Test]
        public void Should_Return_400_When_Creating_Null_Application()
        {
            ApplicationController target = new ApplicationController(_allAppsQuery.Object, _appByIdQuery.Object,
                _saveAppCommand.Object, _updateAppCommand.Object, _deleteAppCommand.Object);

            target.Request = _request;

            target.Create(null).StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }

        [Test]
        public void Should_Return_400_When_Creating_Incomplete_Application()
        {
            ApplicationController target = new ApplicationController(_allAppsQuery.Object, _appByIdQuery.Object,
                _saveAppCommand.Object, _updateAppCommand.Object, _deleteAppCommand.Object);

            target.Request = _request;
            target.ModelState.AddModelError("Name", "Error in Name");
            target.Create(new Application()).StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }
    }
}
