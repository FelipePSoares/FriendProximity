using FluentValidator;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Domain.Handlers.Interfaces;
using FriendProximityAPI.Domain.Repositories;
using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Entities;
using FriendProximityAPI.Shared.Enums;
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

        public GetFriendCommandResult Handler(GetFriendCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GetFriendCommandResult(false, null, command.Notifications.ToList());

            var friendsFormatted = FormatFriends(friendRepository.GetAll().ToList(), command.NumberOfCloserFriends);

            return new GetFriendCommandResult(true, friendsFormatted.Select(f => new FriendCommandResult(f.Name, f.CloseFriends.ToList())).ToList(), new Message(messageType: MessageType.Information, description: "Busca realizada com sucesso!!!" ));
        }

        private List<(string Name, IEnumerable<string> CloseFriends)> FormatFriends(List<Friend> friends, int numberOfCloserFriends)
            => friends
                .Select(friend => (friend.Name, CloseFriends: GetCloseFriends(friend, friends, numberOfCloserFriends))).ToList();
        
        private IEnumerable<string> GetCloseFriends(Friend actualFriend, List<Friend> list, int numberOfCloserFriends)
        {
            if (list == null || list.Count == 0)
                yield break;

            var tree = CreateTree(list);
            list = list.Where(f => f.Id != actualFriend.Id).ToList();

            if (list.Count < numberOfCloserFriends)
                numberOfCloserFriends = list.Count;

            for (int i = 0; i < numberOfCloserFriends; i++)
            {
                var closest = ClosestFriend(actualFriend, tree, list).Key;
                if (closest != Guid.Empty)
                {
                    yield return list.Find(f => f.Id == closest).Name;
                    list.RemoveAt(list.FindIndex(f => f.Id == closest));
                }
            }
        }

        private KeyValuePair<Guid, double> ClosestFriend(Friend actualFriend, Node tree, List<Friend> list)
        {
            Node leaf = GetPointLeaf(actualFriend.Point, tree);

            var closest = KeyValuePair.Create(Guid.Empty, double.MaxValue);

            var closestLocated = ClosestFriendInsideNode(actualFriend.Point, list, leaf, closest);
            
            return closestLocated.Key == Guid.Empty ? closest : closestLocated;
        }

        private KeyValuePair<Guid, double> ClosestFriendInsideNode(Point actualPoint, List<Friend> list, Node node, KeyValuePair<Guid, double> closestFriend)
        {
            closestFriend = SearchClosestFriendInsideNode(actualPoint, list, node, closestFriend);

            var sibling = node.GetSiblingNode();
            if (sibling != null)
                closestFriend = SearchClosestFriendInsideNode(actualPoint, list, sibling, closestFriend);

            return node.ParentNode != null ? ClosestFriendInsideNode(actualPoint, list, node.ParentNode, closestFriend) : closestFriend;
        }

        private KeyValuePair<Guid, double> SearchClosestFriendInsideNode(Point actualPoint, List<Friend> list, Node node, KeyValuePair<Guid, double> closestFriend)
        {
            var friendsInsideNode = node.GetFriendsInsideNode(list);

            if (friendsInsideNode.Count == 0)
                return closestFriend;

            double groupDistance = actualPoint.CalculateGroupDistance(GetGroupSize(friendsInsideNode));

            if (groupDistance > closestFriend.Value)
                return closestFriend;

            var closestLocated = friendsInsideNode.Select(f => KeyValuePair.Create(f.Id, f.Point.CalculateDistance(actualPoint)))
                           .Where(f => f.Value < closestFriend.Value).OrderBy(d => d.Value).FirstOrDefault();
            
            if (closestLocated.Key != Guid.Empty && closestLocated.Value < closestFriend.Value)
                closestFriend = closestLocated;

            return closestFriend;
        }

        private Node GetGroupSize(List<Friend> friendsInsideNode)
        {
            var maxPoint = new Point(friendsInsideNode.Max(f => f.Point.Latitude), friendsInsideNode.Max(f => f.Point.Longitude));
            var minPoint = new Point(friendsInsideNode.Min(f => f.Point.Latitude), friendsInsideNode.Min(f => f.Point.Longitude));
            return new Node(maxPoint, minPoint);
        }

        private Node GetPointLeaf(Point point, Node tree)
            => tree.GetLeaf(point);

        private Node CreateTree(List<Friend> friends)
        {
            var minPoint = new Point(friends.Min(f => f.Point.Latitude), friends.Min(f => f.Point.Longitude));
            var maxPoint = new Point(friends.Max(f => f.Point.Latitude), friends.Max(f => f.Point.Longitude));

            return new Node(maxPoint, minPoint).SplitNode(friends, friends.Count / 3);
        }
    }
}
