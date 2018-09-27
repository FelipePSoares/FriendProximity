using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Handlers.Interfaces;
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
        /// 
        /// </summary>
        [HttpGet]
        public IActionResult GetFriends([FromServices]IGetFriendHandler getFriendHandler)
        {
            return DefineCorrectlyResult(getFriendHandler.Handler(default));
        }

        /// <summary>
        /// Adiciona um amigo.
        /// </summary>
        [HttpPost]
        public IActionResult AddFriend(AddFriendCommand addFriendCommand, [FromServices]IAddFriendHandler addFriendHandler)
        {
            return DefineCorrectlyResult(addFriendHandler.Handler(addFriendCommand));
        }
    }
}