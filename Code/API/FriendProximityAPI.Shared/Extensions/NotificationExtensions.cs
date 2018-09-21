using FluentValidator;
using FriendProximityAPI.Shared.Entities;
using FriendProximityAPI.Shared.Enums;

namespace FriendProximityAPI.Shared.Extensions
{
    public static class NotificationExtensions
    {
        public static Message ToMessage(this Notification notification) 
            => new Message() { MessageType = MessageType.Validation, Property = notification.Property, Description = notification.Message };
    }
}
