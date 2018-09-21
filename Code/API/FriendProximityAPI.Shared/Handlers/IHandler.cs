using FriendProximityAPI.Shared.Commands;

namespace FriendProximityAPI.Shared.Handlers {
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}