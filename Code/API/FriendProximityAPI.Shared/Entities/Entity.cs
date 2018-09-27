using FluentValidator;
using System;

namespace FriendProximityAPI.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        public Entity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
    }
}