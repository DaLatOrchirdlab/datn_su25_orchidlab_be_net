using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.Images;
using orchid_backend_net.Application.Images.Create;
using orchid_backend_net.Application.Images.GetAll;
using orchid_backend_net.Application.Images.GetInfor;
using System.Net.Mime;

namespace orchid_backend_net.API.Controllers.Images
{
    [Route("api/images")]
    [ApiController]
    public class ImgsController(ISender sender, ILogger<ImgsController> logger) : BaseController(sender)
    {
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<ImagesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PageResult<ImagesDTO>>>> GetAllMethods(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromQuery] string? filterReport,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new GetAllImagesQuery(pageNumber, pageSize, filterReport), cancellationToken);
                logger.LogInformation("Received GET request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<PageResult<ImagesDTO>>(result));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while processing GET request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<ImagesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<ImagesDTO>>> GetAllMethods(
          [FromRoute] string id,
           CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new GetImagesInforQuery(id), cancellationToken);
                logger.LogInformation("Received GET request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<ImagesDTO>(result));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while processing GET request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> Create(
          [FromForm] List<IFormFile> files,
          [FromForm] string reportId,
          CancellationToken cancellationToken)
        {
            try
            {
                string result = "";
                if (string.IsNullOrEmpty(reportId))
                {
                    return BadRequest("Report ID cannot be null or empty.");
                }
                foreach (var file in files)
                {
                    if (file.Length == 0)
                    {
                        return BadRequest("File is empty.");
                    }
                    using var stream = file.OpenReadStream();
                    stream.Position = 0;
                    var command = new CreateImageCommand(stream, file.FileName, reportId);
                    result = await sender.Send(command, cancellationToken);
                }

                logger.LogInformation("Received POST request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while processing POST request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }
    }
}
