using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.User;
using orchid_backend_net.Application.User.GetAllUser;

namespace orchid_backend_net.API.Controllers.UserController
{
    [Route("api/user")]
    [ApiController]
    public class UserController(ISender _sender) : BaseController(_sender)
    {
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PageResult<UserDTO>>>> GetAllUser(
            [FromQuery] GetAllUserQuery query, CancellationToken cancellationToken)
        {
            return Ok(await this._sender.Send(query, cancellationToken));
        }
    }
}
