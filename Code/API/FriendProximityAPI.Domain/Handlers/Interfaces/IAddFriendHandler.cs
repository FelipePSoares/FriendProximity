using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Shared.Handlers;

namespace FriendProximityAPI.Domain.Handlers.Interfaces
{
    public interface IAddFriendHandler : IHandler<AddFriendCommand, AddFriendCommandResult>
    {
    }
}
