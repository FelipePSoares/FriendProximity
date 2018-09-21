using FluentAssertions;
using FluentValidator;
using FriendProximityAPI.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FriendProximityAPI.Domain.Test.UnitTest
{
    [TestClass]
    public class FriendTests
    {
        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsNull()
        {
            var expected = new List<Notification>() { new Notification("Name", "Nome não pode ser vazio ou nulo.") };

            var friend = new Friend(null, 0, 0);

            friend.Notifications.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsEmpty()
        {
            var expected = new List<Notification>() { new Notification("Name", "Nome não pode ser vazio ou nulo.") };

            var friend = new Friend(string.Empty, 0, 0);

            friend.Notifications.Should().BeEquivalentTo(expected);
        }
    }
}
