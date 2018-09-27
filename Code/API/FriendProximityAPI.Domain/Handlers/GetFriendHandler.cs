using FluentValidator;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Domain.Handlers.Interfaces;
using FriendProximityAPI.Domain.Repositories;
using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FriendProximityAPI.Domain.Handlers
{
    public class GetFriendHandler :
        Notifiable,
        IGetFriendHandler
    {
        private IFriendRepository friendRepository;

        public GetFriendHandler(IFriendRepository friendRepository)
        {
            this.friendRepository = friendRepository;
        }

        public ICommandResult Handler(GetFriendCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new CommandResult(false, command, command.Notifications.ToList());

            var friendsFormatted = FormatFriends(friendRepository.GetAll().ToList(), 3);

            return new CommandResult(true, friendsFormatted.Select(f => new { name = f.Name, close_friends = f.CloseFriends }).ToList(), default);
        }

        private List<(string Name, List<string> CloseFriends)> FormatFriends(List<Friend> friends, int numberOfCloserFriends)
            => friends
                .Select(friend => (friend.Name, CloseFriends: GetCloseFriends(friend, ListFriendsWithout(friends, friend), numberOfCloserFriends))).ToList();

        private List<Friend> ListFriendsWithout(List<Friend> friends, Friend friend) 
            => friends.Where(f => f.Id != friend.Id).ToList();

        private List<string> GetCloseFriends(Friend actualFriend, List<Friend> list, int numberOfCloserFriends)
        {
            if (list == null || list.Count == 0)
                return default;

            var tree = CreateTree(list);

            var closestFriend = ClosestFriend(actualFriend, tree, list);

            return new List<string>() { list.Find(f => f.Id == closestFriend.Key).Name };
        }

        private KeyValuePair<Guid, double> ClosestFriend(Friend actualFriend, Node tree, List<Friend> list)
        {
            Node leaf = GetPointLeaf(actualFriend.Point, tree);

            var closest = ClosestFriendInsideNode(actualFriend.Point, list, leaf);
            
            return closest;
        }

        private KeyValuePair<Guid, double> ClosestFriendInsideNode(Point actualPoint, List<Friend> list, Node leaf)
        {
            var friends = GetFriendsInsideNode(list, leaf);
            return friends.Select(f => KeyValuePair.Create(f.Id, f.Point.CalculateDistance(actualPoint)))
                .OrderBy(d => d.Value).FirstOrDefault();
        }

        private List<Friend> GetFriendsInsideNode(List<Friend> list, Node node)
            => list.Where(f => node.IsInside(f.Point)).ToList();

        private Node GetPointLeaf(Point point, Node tree)
            => tree.GetLeaf(point);

        private Node CreateTree(List<Friend> list)
        {
            var minPoint = new Point(list.Min(f => f.Point.Latitude), list.Min(f => f.Point.Longitude));
            var maxPoint = new Point(list.Max(f => f.Point.Latitude), list.Max(f => f.Point.Longitude));

            return new Node(maxPoint, minPoint).SplitNode(list.Count / 3);
        }
    }
}
