using FluentValidator;
using FluentValidator.Validation;
using FriendProximityAPI.Shared.Commands;

namespace FriendProximityAPI.Domain.Commands
{
    public class AddFriendCommand : Notifiable, ICommand
    {
        public string Name { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }

        public void Validate()
        {
            this.AddNotifications(new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "Nome não pode ser vazio ou nulo."));
        }
    }
}
