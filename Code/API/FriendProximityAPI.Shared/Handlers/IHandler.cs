using FriendProximityAPI.Shared.Commands;

namespace FriendProximityAPI.Shared.Handlers {
    public interface IHandler<Entry, Result> 
        where Entry : ICommand 
        where Result : ICommandResult
    {
        Result Handler(Entry command);
    }
}