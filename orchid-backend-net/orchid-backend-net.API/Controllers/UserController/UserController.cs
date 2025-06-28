using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.API.Service;
using orchid_backend_net.Application.Authentication.Login;
using orchid_backend_net.Application.Authentication.Refreshtoken.RefreshTokenQuery;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.User;
using orchid_backend_net.Application.User.CreateUser;
using orchid_backend_net.Application.User.GetAllUser;
using orchid_backend_net.Application.User.GetUserInfor;
using System.Net.Mime;

namespace orchid_backend_net.API.Controllers.UserController
{
    [Route("api/user")]
    [ApiController]
    public class UserController(ISender _sender, ILogger<UserController> _logger, JwtService jwtService) : BaseController(_sender)
    {
        //[Authorize(Roles = "Admin,Researcher")]
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
            try
            {
                _logger.LogInformation("Received GET request at {Time}", DateTime.UtcNow);
                return Ok(await this._sender.Send(query, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing GET request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(
            [FromBody] LoginQuery query, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Received POST request at {Time}", DateTime.UtcNow);
                var loginDTO = await this._sender.Send(new LoginQuery(query.Email, query.Password), cancellationToken);
                var token = jwtService.CreateToken(loginDTO.Id, loginDTO.RoleID, loginDTO.RefreshToken, loginDTO.Name);
                token.UserDTO = await _sender.Send(new GetUserInforQuery(loginDTO.Id), cancellationToken);
                var response = new
                {
                    Message = "Login successfully",
                    token.AccessToken,
                    token.RefreshToken,
                    User = token.UserDTO
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing POST request at {Time}", DateTime.UtcNow);
                return BadRequest(new ProblemDetails { Title = "Login failed", Detail = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken(
            [FromBody] string refreshToken, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Received POST request for token refresh at {Time}", DateTime.UtcNow);
                var loginDTO = await this._sender.Send(new RefreshTokenQuery(refreshToken), cancellationToken);
                var token = jwtService.CreateToken(loginDTO.Id, loginDTO.RoleID, loginDTO.RefreshToken, loginDTO.Name);
                token.UserDTO = await _sender.Send(new GetUserInforQuery(loginDTO.Id), cancellationToken);
                var response = new
                {
                    Message = "Login successfully",
                    token.AccessToken,
                    token.RefreshToken,
                    User = token.UserDTO
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing token refresh at {Time}", DateTime.UtcNow);
                return BadRequest(new ProblemDetails { Title = "Token refresh failed", Detail = ex.Message });
            }
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Received POST request at {Time}", DateTime.UtcNow);
                var result = await this._sender.Send(command, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing POST request at {Time}", DateTime.UtcNow);
                return BadRequest(new ProblemDetails { Title = "User creation failed", Detail = ex.Message });
            }
        }
    }
}
