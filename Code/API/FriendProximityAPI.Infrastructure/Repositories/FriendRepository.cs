using System;
using System.Linq;
using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Domain.Repositories;
using FriendProximityAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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
            var result = this.friendProximityContext.Friends.Add(friend);
            this.friendProximityContext.SaveChanges();

            return result.State == EntityState.Added;
        }

        public bool LocationAlreadyExists(int Latitude, int Longitude) 
            => this.friendProximityContext.Friends
                .Any(f => f.Point.Latitude == Latitude && f.Point.Longitude == Longitude);

        public IQueryable<Friend> GetAll() => this.friendProximityContext.Friends;
    }
}
