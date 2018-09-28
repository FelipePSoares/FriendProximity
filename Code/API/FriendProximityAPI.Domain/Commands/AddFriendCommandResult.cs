using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Entities;
using Newtonsoft.Json;

namespace FriendProximityAPI.Domain.Commands
{
    public class AddFriendCommandResult : ICommandResult
    {
        public AddFriendCommandResult(bool isSuccessful, bool data, Messages messages)
        {
            this.IsSuccessful = isSuccessful;
            this.Data = data;
            this.Messages = messages;
        }

        [JsonProperty("data")]
        public bool Data { get; set; }

        [JsonProperty("is_successful")]
        public bool IsSuccessful { get; }

        [JsonProperty("messages")]
        public Messages Messages { get; }
    }
}
