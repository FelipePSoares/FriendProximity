using FriendProximityAPI.Domain.Entities;
using System;
using System.Linq;

namespace FriendProximityAPI.Domain.Repositories
{
    public interface IFriendRepository
    {
        bool Add(Friend friend);

        IQueryable<Friend> GetAll();

        bool LocationAlreadyExists(int Latitude, int Longitude);
    }
}