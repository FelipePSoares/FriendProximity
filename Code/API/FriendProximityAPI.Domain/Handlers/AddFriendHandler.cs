using FluentValidator;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Domain.Handlers.Interfaces;
using FriendProximityAPI.Domain.Repositories;
using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Entities;
using FriendProximityAPI.Shared.Enums;
using FriendProximityAPI.Shared.Handlers;
using System.Linq;

namespace FriendProximityAPI.Domain.Handlers
{
    public class AddFriendHandler :
        Notifiable,
        IAddFriendHandler
    {
        private IFriendRepository friendRepository;

        public AddFriendHandler(IFriendRepository friendRepository)
        {
            this.friendRepository = friendRepository;
        }

        public ICommandResult Handler(AddFriendCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new CommandResult(false, command, command.Notifications.ToList());
            
            if (friendRepository.LocationAlreadyExists(Latitude: command.Latitude, Longitude: command.Longitude))
                AddNotification("Location", "Já existe um amigo cadastrado nesta localização.");

            if (Invalid)
                return new CommandResult(false, command, Notifications.ToList());

            var friend = new Friend(command.Name, new Point(command.Latitude, command.Longitude));
            
            AddNotifications(friend);
            
            if (Invalid)
                return new CommandResult(false, command, Notifications.ToList());

            friendRepository.Add(friend); 

            return new CommandResult(true, null, new Message() { MessageType = MessageType.Information, Description = "Amigo cadastrado com sucesso!" });
        }
    }
}
