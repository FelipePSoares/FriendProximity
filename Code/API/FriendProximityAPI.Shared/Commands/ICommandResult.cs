using FriendProximityAPI.Shared.Entities;

namespace FriendProximityAPI.Shared.Commands
{
    public interface ICommandResult
    {
        bool IsSuccessful { get; }
        object Data { get; }
        Messages Messages { get; }
    }
}