using FriendProximityAPI.Domain.Commands;
using FriendProximityAPI.Domain.Handlers.Interfaces;
using FriendProximityAPI.Shared.Commands;
using FriendProximityAPI.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FriendProximityAPI.Controllers
{
    /// <summary>
    /// API para adição e busca dos amigos.
    /// </summary>
    [Produces("application/json")]
    public class FriendsController : BaseController
    {
        [HttpGet("/friends/")]
        [SwaggerOperation(
            Summary = "Lista amigos.",
            Description = "Lista de amigos com os amigos mais proximos.",
            OperationId = "",
            Tags = new[] { "Amigos" }
        )]
        [SwaggerResponse(200, "Sucesso!", typeof(GetFriendCommandResult))]
        [SwaggerResponse(400, "Parametro inválido", typeof(IActionResult))]
        [SwaggerResponse(500, "Erro Interno", typeof(IActionResult))]
        public IActionResult GetFriends([FromServices]IGetFriendHandler getFriendHandler)
        {
            return DefineCorrectlyResult(getFriendHandler.Handler(new GetFriendCommand(3)));
        }
        
        [HttpPost("/friends/")]
        [SwaggerOperation(
                Summary = "Adiciona amigo.",
                Description = "Adiciona amigo.",
                OperationId = "",
                Tags = new[] { "Amigos" }
            )]
        [SwaggerResponse(200, "Sucesso!", typeof(AddFriendCommandResult))]
        [SwaggerResponse(400, "Parametro inválido", typeof(IActionResult))]
        [SwaggerResponse(500, "Erro Interno", typeof(IActionResult))]
        public IActionResult AddFriend([FromBody]AddFriendCommand addFriendCommand, [FromServices]IAddFriendHandler addFriendHandler)
        {
            return DefineCorrectlyResult(addFriendHandler.Handler(addFriendCommand));
        }
    }
}