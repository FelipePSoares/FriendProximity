using FakeItEasy;
using FriendProximityAPI.Controllers;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Handlers;
using FriendProximityAPI.Domain.Handlers.Interfaces;
using FriendProximityAPI.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendProximityAPI.Test.UnitTests
{
    [TestClass]
    public class FriendsControllerTests
    {
        private FriendsController friendsController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.friendsController = new FriendsController();
        }

        [TestMethod]
        public void ShouldCallAddFriendHandlerCorrectly()
        {
            var addFriendCommand = new AddFriendCommand() { Name = "Lindsey", Latitude = 321, Longitude = 987 };
            var addFriendHandler = A.Fake<IAddFriendHandler>();

            this.friendsController.AddFriend(addFriendCommand, addFriendHandler);

            A.CallTo(() 
                => addFriendHandler
                .Handler(A<AddFriendCommand>.That.Matches(f => f.Name == "Lindsey" && f.Latitude == 321 && f.Longitude == 987)))
                .MustHaveHappenedOnceExactly();
        }
    }
}
