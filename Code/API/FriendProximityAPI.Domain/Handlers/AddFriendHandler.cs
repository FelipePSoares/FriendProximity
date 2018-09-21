using FluentValidator;
using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Repositories;
using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Handlers;
using System;

namespace FriendProximityAPI.Domain.Handlers
{
    public class AddFriendHandler :
        Notifiable,
        IHandler<AddFriendCommand>
    {
        private IFriendRepository friendRepository;

        public AddFriendHandler(IFriendRepository friendRepository)
        {
            this.friendRepository = friendRepository;
        }

        public ICommandResult Handle(AddFriendCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
