using FakeItEasy;
using FluentAssertions;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Controllers;
using FriendProximityAPI.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace FriendProximityAPI.Shared.Test.UnitTests
{
    [TestClass]
    public class BaseControllerTests
    {
        private TestController baseController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.baseController = new TestController();
        }

        [TestMethod]
        public void ShouldReturnOkResultWhenIsSuccessfulResult()
        {
            var commandResult = A.Fake<ICommandResult>();
            A.CallTo(() => commandResult.IsSuccessful).Returns(true);
            
            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenIsFailResultWithNoContent()
        {
            var commandResult = A.Fake<ICommandResult>();
            A.CallTo(() => commandResult.IsSuccessful).Returns(false);

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<BadRequestResult>();
            (result as BadRequestResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenIsFailResultWithTypeValidation()
        {
            var commandResult = A.Fake<ICommandResult>();
            A.CallTo(() => commandResult.IsSuccessful).Returns(false);
            A.CallTo(() => commandResult.Messages).Returns(new Message() { MessageType = Enums.MessageType.Validation });
            
            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
            (result as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenIsFailResultWithTypeCritical()
        {
            var commandResult = A.Fake<ICommandResult>();
            A.CallTo(() => commandResult.IsSuccessful).Returns(false);
            A.CallTo(() => commandResult.Messages).Returns(new Message() { MessageType = Enums.MessageType.Critical });

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
            (result as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenIsFailResultWithTypedInformation()
        {
            var commandResult = A.Fake<ICommandResult>();
            A.CallTo(() => commandResult.IsSuccessful).Returns(false);
            A.CallTo(() => commandResult.Messages).Returns(new Message() { MessageType = Enums.MessageType.Information });

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
            (result as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }

    public class TestController : BaseController
    {
        public new ActionResult DefineCorrectlyResult(ICommandResult commandResult)
        {
            return base.DefineCorrectlyResult(commandResult);
        }
    }
}