using FriendProximityAPI.Shared.Enums;
using System.Collections.Generic;

namespace FriendProximityAPI.Shared.Entities
{
    public class Message
    {
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

        public static implicit operator Messages(Message message) => new Messages(message);
    }
}
