using FluentValidator.Validation;
using FriendProximityAPI.Shared.Entities;
using System;

namespace FriendProximityAPI.Domain.Entities
{
    public class Friend : Entity
    {
        public Friend(string name, Point point, Guid? id = null) : base(id)
        {
            this.Name = name;
            this.Point = point;

            AddNotifications(new ValidationContract()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "Nome não pode ser vazio ou nulo.")
                .IsNotNull(point, "Ponto", "Ponto não pode ser nulo.")
            );
        }

        public string Name { get; private set; }
        public Point Point { get; private set; }
    }
}
