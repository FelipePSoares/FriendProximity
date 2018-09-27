using FakeItEasy;
using FluentAssertions;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Domain.Handlers;
using FriendProximityAPI.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FriendProximityAPI.Infrastructure.Test.UnitTests
{
    [TestClass]
    public class GetFriendHandlerTests
    {
        private IFriendRepository friendRepository;
        private GetFriendHandler getFriendHandler;

        [TestInitialize]
        public void TestInitialize()
        {
            friendRepository = A.Fake<IFriendRepository>();
            getFriendHandler = new GetFriendHandler(friendRepository);
        }

        [TestMethod]
        public void ShouldReturnAEmptyListOfFriends()
        {
            var listFriends = new List<Friend>()
            {
            };

            A.CallTo(() => friendRepository.GetAll())
                .Returns(listFriends.AsQueryable());

            var result = this.getFriendHandler.Handler(new GetFriendCommand());

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[]");
        }

        [TestMethod]
        public void ShouldReturnAFriendWithEmptyListIfHasOneFriend()
        {
            var listFriends = new List<Friend>()
            {
                new Friend("Lindsey", new Point(1, 1))
            };

            A.CallTo(() => friendRepository.GetAll())
                .Returns(listFriends.AsQueryable());

            var result = this.getFriendHandler.Handler(new GetFriendCommand());

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":null}]");
        }

        [TestMethod]
        public void ShouldReturnTwoFriendsWithAListWithOneFriend()
        {
            var listFriends = new List<Friend>()
            {
                new Friend("Lindsey", new Point(1, 1)),
                new Friend("Eduardo", new Point(2, 4))
            };

            A.CallTo(() => friendRepository.GetAll())
                .Returns(listFriends.AsQueryable());

            var result = this.getFriendHandler.Handler(new GetFriendCommand());

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":[""Eduardo""]},{""name"":""Eduardo"",""close_friends"":[""Lindsey""]}]");
        }

        [TestMethod]
        public void ShouldReturnThreeFriendsWithAListWithTwoOrdenedFriends()
        {
            var listFriends = new List<Friend>()
            {
                new Friend("Lindsey", new Point(1, 1)),
                new Friend("Eduardo", new Point(2, 4)),
                new Friend("Roberta", new Point(7, 2))
            };

            A.CallTo(() => friendRepository.GetAll())
                .Returns(listFriends.AsQueryable());

            var result = this.getFriendHandler.Handler(new GetFriendCommand());

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":[""Eduardo"", ""Roberta""]},{""name"":""Eduardo"",""close_friends"":[""Lindsey"",""Roberta""]},{""name"":""Roberta"",""close_friends"":[""Eduardo"",""Lindsey""]}]");
        }

        [TestMethod]
        public void ShouldReturnFourFriendsWithAListWithThreeOrdenedFriends()
        {
            var listFriends = new List<Friend>()
            {
                new Friend("Lindsey", new Point(1, 1)),
                new Friend("Eduardo", new Point(2, 4)),
                new Friend("Roberta", new Point(7, 2)),
                new Friend("Carlos", new Point(3, 6))
            };

            A.CallTo(() => friendRepository.GetAll())
                .Returns(listFriends.AsQueryable());

            var result = this.getFriendHandler.Handler(new GetFriendCommand());

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":[""Eduardo"",""Carlos"",""Roberta""]},{""name"":""Eduardo"",""close_friends"":[""Carlos"",""Lindsey"",""Roberta""]},{""name"":""Roberta"",""close_friends"":[""Eduardo"",""Lindsey"",""Carlos""]},{""name"":""Carlos"",""close_friends"":[""Eduardo"",""Lindsey"",""Roberta""]}]");
        }

        [TestMethod]
        public void ShouldReturnFiveFriendsWithAListWithThreeOrdenedFriends()
        {
            var listFriends = new List<Friend>()
            {
                new Friend("Lindsey", new Point(1, 1)),
                new Friend("Eduardo", new Point(2, 4)),
                new Friend("Roberta", new Point(7, 2)),
                new Friend("Carlos", new Point(3, 6)),
                new Friend("Jonathan", new Point(8, 3))
            };

            A.CallTo(() => friendRepository.GetAll())
                .Returns(listFriends.AsQueryable());

            var result = this.getFriendHandler.Handler(new GetFriendCommand());

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":[""Eduardo"",""Carlos"",""Roberta""]},{""name"":""Eduardo"",""close_friends"":[""Carlos"",""Lindsey"",""Roberta""]},{""name"":""Roberta"",""close_friends"":[""Jonathan"",""Eduardo"",""Lindsey""]},{""name"":""Carlos"",""close_friends"":[""Eduardo"",""Lindsey"",""Jonathan""]},{""name"":""Jonathan"",""close_friends"":[""Roberta"",""Eduardo"",""Carlos""]}]");
        }
    }
}