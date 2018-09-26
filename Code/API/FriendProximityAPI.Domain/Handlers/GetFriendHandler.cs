using FluentValidator;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Handlers;
using System.Linq;

namespace FriendProximityAPI.Domain.Handlers
{
    public class GetFriendHandler :
        Notifiable,
        IHandler<GetFriendCommand>
    {
        public ICommandResult Handler(GetFriendCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new CommandResult(false, command, command.Notifications.ToList());

            return default;
        }
    }
}
