using MediatR;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.Application.Seedling.CreateSeedling;
using System.Net.Mime;

namespace orchid_backend_net.API.Controllers.Seedling
{
    [Route("api/seedling")]
    [ApiController]
    public class SeedlingController(ISender _sender, ILogger<SeedlingController> _logger) : ControllerBase
    {

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> CreateSeedling([FromBody] CreateSeedlingCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _sender.Send(command, cancellationToken);
                _logger.LogInformation("Received POST request at {Time}", DateTime.UtcNow);
                return CreatedAtAction(nameof(CreateSeedling), new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing POST request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }
    }
}
