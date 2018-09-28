using FluentValidator;
using FriendProximityAPI.Shared.Enums;
using FriendProximityAPI.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace FriendProximityAPI.Shared.Entities
{
    public class Message
    {
        public Message() { }

        public Message([Optional]string code, [Optional]MessageType messageType, [Optional]string property, [Optional]string description)
        {
            this.Code = code;
            this.MessageType = messageType;
            this.Property = property;
            this.Description = description;
        }

        public string Code { get; set; }
        public MessageType MessageType { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
    }

    public class Messages : List<Message>
    {
        public Messages() { }
        public Messages(Message message) => this.Add(message);
        public Messages(List<Message> messages) => this.AddRange(messages);
        public Messages(Notification notification) => this.Add(notification.ToMessage());
        public Messages(IReadOnlyCollection<Notification> notifications)
            => this.AddRange(notifications.Select(notification => notification.ToMessage()).ToList());

        public static implicit operator Messages(Message message) => new Messages(message);
        public static implicit operator Messages(Notification notification) => new Messages(notification);
        public static implicit operator Messages(List<Notification> notifications) => new Messages(notifications);
    }
}
