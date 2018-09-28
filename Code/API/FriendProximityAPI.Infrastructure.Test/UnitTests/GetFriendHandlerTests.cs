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

            var result = this.getFriendHandler.Handler(new GetFriendCommand(3));

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

            var result = this.getFriendHandler.Handler(new GetFriendCommand(3));

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":[]}]");
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

            var result = this.getFriendHandler.Handler(new GetFriendCommand(3));

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

            var result = this.getFriendHandler.Handler(new GetFriendCommand(3));
            
            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":[""Eduardo"",""Roberta""]},{""name"":""Eduardo"",""close_friends"":[""Lindsey"",""Roberta""]},{""name"":""Roberta"",""close_friends"":[""Eduardo"",""Lindsey""]}]");
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

            var result = this.getFriendHandler.Handler(new GetFriendCommand(3));

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":[""Eduardo"",""Carlos"",""Roberta""]},{""name"":""Eduardo"",""close_friends"":[""Carlos"",""Lindsey"",""Roberta""]},{""name"":""Roberta"",""close_friends"":[""Eduardo"",""Carlos"",""Lindsey""]},{""name"":""Carlos"",""close_friends"":[""Eduardo"",""Lindsey"",""Roberta""]}]");
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

            var result = this.getFriendHandler.Handler(new GetFriendCommand(3));

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""Lindsey"",""close_friends"":[""Eduardo"",""Carlos"",""Roberta""]},{""name"":""Eduardo"",""close_friends"":[""Carlos"",""Lindsey"",""Roberta""]},{""name"":""Roberta"",""close_friends"":[""Jonathan"",""Eduardo"",""Carlos""]},{""name"":""Carlos"",""close_friends"":[""Eduardo"",""Lindsey"",""Roberta""]},{""name"":""Jonathan"",""close_friends"":[""Roberta"",""Carlos"",""Eduardo""]}]");
        }

        [TestMethod]
        public void ShouldReturnAllFriendsWithAListWithThreeOrdenedFriends()
        {
            var listFriends = new List<Friend>()
            {
                new Friend("A", new Point(1, 1)),
                new Friend("B", new Point(2, 4)),
                new Friend("C", new Point(7, 2)),
                new Friend("D", new Point(3, 6)),
                new Friend("E", new Point(8, 3)),
                new Friend("F", new Point(4, 6)),
                new Friend("G", new Point(2, 8)),
                new Friend("H", new Point(7, 6)),
                new Friend("I", new Point(5, 2)),
                new Friend("J", new Point(9, 3)),
                new Friend("K", new Point(5, 4)),
                new Friend("L", new Point(2, 7)),
                new Friend("M", new Point(5, 1))
            };

            A.CallTo(() => friendRepository.GetAll())
                .Returns(listFriends.AsQueryable());

            var result = this.getFriendHandler.Handler(new GetFriendCommand(3));

            JsonConvert.SerializeObject(result.Data).Should().Be(@"[{""name"":""A"",""close_friends"":[""B"",""M"",""I""]},{""name"":""B"",""close_friends"":[""D"",""F"",""L""]},{""name"":""C"",""close_friends"":[""E"",""I"",""J""]},{""name"":""D"",""close_friends"":[""F"",""L"",""B""]},{""name"":""E"",""close_friends"":[""J"",""C"",""I""]},{""name"":""F"",""close_friends"":[""D"",""L"",""K""]},{""name"":""G"",""close_friends"":[""L"",""D"",""F""]},{""name"":""H"",""close_friends"":[""K"",""F"",""E""]},{""name"":""I"",""close_friends"":[""M"",""C"",""K""]},{""name"":""J"",""close_friends"":[""E"",""C"",""H""]},{""name"":""K"",""close_friends"":[""I"",""F"",""C""]},{""name"":""L"",""close_friends"":[""G"",""D"",""F""]},{""name"":""M"",""close_friends"":[""I"",""C"",""K""]}]");
        }

        [TestMethod]
        public void ShouldReturnCorrectlyForManyFriends()
        {
            var random = new Random();
            var listFriends = new List<Friend>();

            for (int i = 0; i < 1000000; i++)
                listFriends.Add(new Friend($"{i}", new Point(random.Next(0, 1000), random.Next(0, 1000))));

            A.CallTo(() => friendRepository.GetAll())
                .Returns(listFriends.AsQueryable());

            var result = this.getFriendHandler.Handler(new GetFriendCommand(3));

            result.IsSuccessful.Should().BeTrue();
        }
    }
}