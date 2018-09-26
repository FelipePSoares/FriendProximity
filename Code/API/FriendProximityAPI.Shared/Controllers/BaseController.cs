using System;
using System.Collections.Generic;
using System.Linq;
using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Entities;
using FriendProximityAPI.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FriendProximityAPI.Shared.Controllers
{
    public class BaseController : ControllerBase
    {
        private ActionResult GetCorrectlyResult(ICommandResult commandResult)
        {
            return BadRequest(commandResult);
        }

        private bool HasMessageTypedError(ICommandResult commandResult)
            => GetMessagesTypedError(commandResult)?.Any() ?? false;

        private List<Message> GetMessagesTypedError(ICommandResult commandResult)
            => commandResult?.Messages?.Where(m => m.MessageType != MessageType.None)?.ToList();

        protected ActionResult DefineCorrectlyResult(ICommandResult commandResult)
        {
            if (commandResult.IsSuccessful)
            {
                if (commandResult.Data == null)
                    return NoContent();
                else
                    return Ok(commandResult);
            }
            else if (HasMessageTypedError(commandResult))
                return GetCorrectlyResult(commandResult);
            else
                return BadRequest();
        }
    }
}