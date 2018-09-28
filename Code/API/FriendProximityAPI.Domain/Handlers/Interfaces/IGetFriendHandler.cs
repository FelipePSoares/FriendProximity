using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Shared.Handlers;

namespace FriendProximityAPI.Domain.Handlers.Interfaces
{
    public interface IGetFriendHandler : IHandler<GetFriendCommand, GetFriendCommandResult>
    {
    }
}
