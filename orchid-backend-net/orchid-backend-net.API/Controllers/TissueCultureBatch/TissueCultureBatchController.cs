using MediatR;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.TissueCultureBatch;
using orchid_backend_net.Application.TissueCultureBatch.GetAllTissueCultureBatch;
using System.Net.Mime;

namespace orchid_backend_net.API.Controllers.TissueCultureBatch
{
    [Route("api/tissue-culture-batch")]
    [ApiController]
    public class TissueCultureBatchController(ISender _sender, ILogger<TissueCultureBatchController> _logger) : BaseController(_sender)
    {

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<TissueCultureBatchDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<TissueCultureBatchDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PageResult<TissueCultureBatchDTO>>>> GetAllExperimentLog(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            CancellationToken cancellationToken)
        {
            try
            {

                var result = await this._sender.Send(new GetAllTissueCultureBatchQuery(pageNumber, pageSize), cancellationToken);
                _logger.LogInformation("Received GET request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<PageResult<TissueCultureBatchDTO>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing GET request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }
    }
}
