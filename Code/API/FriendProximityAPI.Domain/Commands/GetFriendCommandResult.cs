using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FriendProximityAPI.Domain.Commands
{
    public class GetFriendCommandResult : ICommandResult
    {
        public GetFriendCommandResult(bool isSuccessful, List<FriendCommandResult> data, Messages messages)
        {
            this.IsSuccessful = isSuccessful;
            this.Data = data;
            this.Messages = messages;
        }

        [JsonProperty("data")]
        public List<FriendCommandResult> Data { get; set; }

        [JsonProperty("is_successful")]
        public bool IsSuccessful { get; }

        [JsonProperty("messages")]
        public Messages Messages { get; }
    }
}
