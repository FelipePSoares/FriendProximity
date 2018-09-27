using FakeItEasy;
using FluentAssertions;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Domain.Handlers;
using FriendProximityAPI.Domain.Repositories;
using FriendProximityAPI.Shared.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendProximityAPI.Domain.Test.UnitTests
{
    [TestClass]
    public class AddFriendHandlerTests
    {
        private IFriendRepository friendRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.friendRepository = A.Fake<IFriendRepository>();
        }

        [TestMethod]
        public void ShouldReturnErrorMessageWhenHasAnotherFriendInSameLocation()
        {
            A.CallTo(() => this.friendRepository.LocationAlreadyExists(A<int>.Ignored, A<int>.Ignored))
                .Returns(true);

            var addFriendHandler = new AddFriendHandler(this.friendRepository);

            var command = new AddFriendCommand()
            {
                Name = "Some Name",
                Latitude = 1234,
                Longitude = 4321
            };

            var result = addFriendHandler.Handler(command);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should()
                .Contain(m => 
                    m.MessageType == MessageType.Validation && 
                    m.Property == "Location" &&
                    m.Description == "Já existe um amigo cadastrado nesta localização.");
        }
        
        [TestMethod]
        public void ShouldReturnErrorMessageWhenCommandIsInvalid()
        {
            var addFriendHandler = new AddFriendHandler(this.friendRepository);
            
            var command = new AddFriendCommand()
            {
                Name = null,
                Latitude = 1234,
                Longitude = 4321
            };

            var result = addFriendHandler.Handler(command);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should()
                .Contain(m =>
                    m.MessageType == MessageType.Validation &&
                    m.Property == "Name" &&
                    m.Description == "Nome não pode ser vazio ou nulo.");
        }

        [TestMethod]
        public void ShouldReturnSuccessfulWhenNotExistsNotifications()
        {
            A.CallTo(() => this.friendRepository.LocationAlreadyExists(A<int>.Ignored, A<int>.Ignored)).Returns(false);
            A.CallTo(() => this.friendRepository.Add(A<Friend>.Ignored)).Returns(true);

            var addFriendHandler = new AddFriendHandler(this.friendRepository);

            var command = new AddFriendCommand()
            {
                Name = "Some Name",
                Latitude = 1234,
                Longitude = 4321
            };

            var result = addFriendHandler.Handler(command);

            result.IsSuccessful.Should().BeTrue();
            result.Messages.Should()
                .Contain(m =>
                    m.MessageType == MessageType.Information &&
                    m.Description == "Amigo cadastrado com sucesso!");
        }
    }
}
