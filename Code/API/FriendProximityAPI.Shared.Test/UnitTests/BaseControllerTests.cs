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
            var commandResult = new CommandResult(true, true);

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<OkObjectResult>();
            (result as OkObjectResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
            (result as OkObjectResult).Value.Should().BeEquivalentTo(new CommandResult(true, true));
        }

        [TestMethod]
        public void ShouldReturnOkResultWhenIsSuccessfulResultWithNoContent()
        {
            var commandResult = new CommandResult(true);

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<NoContentResult>();
            (result as NoContentResult).StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenIsFailResultWithNoContent()
        {
            var commandResult = new CommandResult(false);

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<BadRequestResult>();
            (result as BadRequestResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenIsFailResultWithTypeValidation()
        {
            var commandResult = new CommandResult(false, true, new Message() { MessageType = Enums.MessageType.Validation });

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
            (result as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result as BadRequestObjectResult).Value.Should().BeEquivalentTo(new CommandResult(false, true, new Message() { MessageType = Enums.MessageType.Validation }));
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenIsFailResultWithTypeCritical()
        {
            var commandResult = new CommandResult(false, true, new Message() { MessageType = Enums.MessageType.Critical });

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
            (result as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result as BadRequestObjectResult).Value.Should().BeEquivalentTo(new CommandResult(false, true, new Message() { MessageType = Enums.MessageType.Critical }));
        }

        [TestMethod]
        public void ShouldReturnBadRequestWhenIsFailResultWithTypedInformation()
        {
            var commandResult = new CommandResult(false, true, new Message() { MessageType = Enums.MessageType.Information });

            var result = this.baseController.DefineCorrectlyResult(commandResult);

            result.Should().BeAssignableTo<BadRequestObjectResult>();
            (result as BadRequestObjectResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result as BadRequestObjectResult).Value.Should().BeEquivalentTo(new CommandResult(false, true, new Message() { MessageType = Enums.MessageType.Information }));
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