using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Handlers;
using FriendProximityAPI.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FriendProximityAPI.Controllers
{
    /// <summary>
    /// API para adição e busca dos amigos.
    /// </summary>
    [ApiController]
    public class FriendsController : BaseController
    {
        /// <summary>
        /// Adiciona um amigo.
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddFriend(AddFriendCommand addFriendCommand, [FromServices]AddFriendHandler addFriendHandler)
        {
            return DefineCorrectlyResult(addFriendHandler.Handle(addFriendCommand));
        }
    }
}