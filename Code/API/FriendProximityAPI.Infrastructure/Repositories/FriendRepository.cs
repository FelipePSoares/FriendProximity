using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Domain.Repositories;
using FriendProximityAPI.Infrastructure.Context;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace FriendProximityAPI.Infrastructure.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly FriendProximityContext friendProximityContext;

        public FriendRepository(FriendProximityContext friendProximityContext)
        {
            this.friendProximityContext = friendProximityContext;
        }

        public bool Add(Friend friend)
        {
            this.friendProximityContext.Friend.InsertOne(friend);

            return true;
        }

        public bool LocationAlreadyExists(int Latitude, int Longitude) 
            => this.friendProximityContext.Friend
                .Find(f => f.Point.Latitude == Latitude && f.Point.Longitude == Longitude).Any();

        public ICollection<Friend> GetAll() => this.friendProximityContext.Friend.Find(_ => true).ToList();
    }
}
