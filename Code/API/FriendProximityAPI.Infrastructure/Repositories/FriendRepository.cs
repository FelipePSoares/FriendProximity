using System;
using System.Linq;
using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Domain.Repositories;

namespace FriendProximityAPI.Infrastructure.Repositories
{
    public class FriendRepository : IFriendRepository
    {


        public bool Add(Friend friend)
        {
            throw new NotImplementedException();
        }

        public bool LocationAlreadyExists(int Latitude, int Longitude)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Friend> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
