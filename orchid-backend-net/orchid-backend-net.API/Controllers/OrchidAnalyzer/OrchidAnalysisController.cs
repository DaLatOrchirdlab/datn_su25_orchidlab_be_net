using MediatR;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.Application.OrchidAnalysis;
using orchid_backend_net.Domain.Entities;
using System.Net.Mime;

namespace orchid_backend_net.API.Controllers.OrchidAnalyzer
{
    [Route("api/orchid-analysis")]
    [ApiController]
    public class OrchidAnalysisController(ISender _sender, ILogger<OrchidAnalysisController> _logger) : BaseController(_sender)
    {
        [HttpPost("analyze")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<OrchidAnalysisResult>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<OrchidAnalysisResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<OrchidAnalysisResult>>> AnalyzeOrchidImage(
            IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("Image file is required.");

            using var stream = imageFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var command = new OrchidAnalyzerCommand { ImageBytes = imageBytes };
            var result = await _sender.Send(command);
            _logger.LogInformation("Received GET request at {Time}", DateTime.UtcNow);
            return Ok(result);
        }
    }
}
