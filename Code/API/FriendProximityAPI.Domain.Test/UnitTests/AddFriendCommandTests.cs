    using FluentAssertions;
using FluentValidator;
using FriendProximityAPI.Domain.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FriendProximityAPI.Domain.Test.UnitTests
{
    [TestClass]
    public class AddFriendCommandTests
    {
        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsNull()
        {
            var expected = new List<Notification>() { new Notification("Name", "Nome não pode ser vazio ou nulo.") };

            var friend = new AddFriendCommand()
            {
                Name = null,
                Latitude = 0,
                Longitude = 0
            };
            friend.Validate();

            friend.Valid.Should().BeFalse();
            friend.Notifications.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsEmpty()
        {
            var expected = new List<Notification>() { new Notification("Name", "Nome não pode ser vazio ou nulo.") };

            var friend = new AddFriendCommand()
            {
                Name = string.Empty,
                Latitude = 0,
                Longitude = 0
            };
            friend.Validate();

            friend.Valid.Should().BeFalse();
            friend.Notifications.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ShouldReturnValidWhenNotExistNotifications()
        {
            var friend = new AddFriendCommand()
            {
                Name = "Some Name",
                Latitude = 0,
                Longitude = 0
            };
            friend.Validate();

            friend.Valid.Should().BeTrue();
        }
    }
}
