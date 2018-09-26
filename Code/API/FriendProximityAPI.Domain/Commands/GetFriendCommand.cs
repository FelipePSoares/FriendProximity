using FluentValidator;
using FriendProximityAPI.Shared.Commands;

namespace FriendProximityAPI.Domain.Commands
{
    public class GetFriendCommand : Notifiable, ICommand
    {
        public void Validate() { }
    }
}
