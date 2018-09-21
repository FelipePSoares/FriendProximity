using FluentValidator.Validation;
using FriendProximityAPI.Shared.Entities;

namespace FriendProximityAPI.Domain.Entities
{
    public class Friend : Entity
    {
        public Friend(string name, int Latitude, int Longitude)
        {
            this.Name = name;
            this.Latitude = Latitude;
            this.Longitude = Longitude;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "Nome não pode ser vazio ou nulo.")
            );
        }

        public string Name { get; private set; }
        public int Latitude { get; private set; }
        public int Longitude { get; private set; }
    }
}
