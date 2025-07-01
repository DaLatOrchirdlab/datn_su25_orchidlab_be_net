using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.Method;
using orchid_backend_net.Application.Method.GetAllMethod;

namespace orchid_backend_net.API.Controllers.Method
{
    [Route("api/[controller]")]
    [ApiController]
    public class MethodController(ISender _sender, ILogger<MethodController> logger) : BaseController(_sender)
    {
        [HttpGet]
        [ProducesResponseType(typeof(JsonResponse<MethodDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PageResult<MethodDTO>>>> GetAllMethods(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._sender.Send(new GetAllMethodQuery(pageNumber, pageSize), cancellationToken);
                logger.LogInformation("Received GET request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<PageResult<MethodDTO>>(result));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while processing GET request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }
    }
}
