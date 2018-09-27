using FluentAssertions;
using FluentValidator;
using FriendProximityAPI.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FriendProximityAPI.Domain.Test.UnitTests
{
    [TestClass]
    public class FriendTests
    {
        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsNull()
        {
            var expected = new List<Notification>() { new Notification("Name", "Nome não pode ser vazio ou nulo.") };

            var friend = new Friend(null, new Point(latitude: 0, longitude: 0));

            friend.Valid.Should().BeFalse();
            friend.Notifications.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsEmpty()
        {
            var expected = new List<Notification>() { new Notification("Name", "Nome não pode ser vazio ou nulo.") };

            var friend = new Friend(string.Empty, new Point(latitude: 0, longitude: 0));

            friend.Valid.Should().BeFalse();
            friend.Notifications.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ShouldReturnValidWhenNotExistNotifications()
        {
            var friend = new Friend("Some Name", new Point(latitude: 0, longitude: 0));

            friend.Valid.Should().BeTrue();
        }
    }
}
