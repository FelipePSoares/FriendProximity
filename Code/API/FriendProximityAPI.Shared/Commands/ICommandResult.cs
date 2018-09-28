using FriendProximityAPI.Shared.Entities;

namespace FriendProximityAPI.Shared.Commands
{
    public interface ICommandResult
    {
        bool IsSuccessful { get; }
        Messages Messages { get; }
    }
}