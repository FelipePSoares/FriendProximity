﻿using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Entities;

namespace FriendProximityAPI.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool isSuccessful, object data, Messages messages)
        {
            this.IsSuccessful = isSuccessful;
            this.Data = data;
            this.Messages = messages;
        }

        public bool IsSuccessful { get; }

        public object Data { get; }

        public Messages Messages { get; }
    }
}