using Newtonsoft.Json;
using System.Collections.Generic;

namespace FriendProximityAPI.Domain.Commands
{
    public class FriendCommandResult
    {
        public FriendCommandResult(string name, ICollection<string> closeFriends)
        {
            this.Name = name;
            this.CloseFriends = closeFriends;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("close_friends")]
        public ICollection<string> CloseFriends { get; set; }
    }
}