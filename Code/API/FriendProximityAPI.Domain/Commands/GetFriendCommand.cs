using FluentValidator;
using FriendProximityAPI.Shared.Commands;

namespace FriendProximityAPI.Domain.Commands
{
    public class GetFriendCommand : Notifiable, ICommand
    {
        public GetFriendCommand(int numberOfCloserFriends)
        {
            this.NumberOfCloserFriends = numberOfCloserFriends;
        }

        public int NumberOfCloserFriends { get; set; }

        public void Validate() { }
    }
}
