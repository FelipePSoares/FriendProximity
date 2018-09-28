using FriendProximityAPI.Domain.Entities;
using System.Collections.Generic;

namespace FriendProximityAPI.Domain.Repositories
{
    public interface IFriendRepository
    {
        bool Add(Friend friend);

        ICollection<Friend> GetAll();

        bool LocationAlreadyExists(int Latitude, int Longitude);
    }
}