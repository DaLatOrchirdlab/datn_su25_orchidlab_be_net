using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.Tasks;
using orchid_backend_net.Application.Tasks.CreateTask;
using orchid_backend_net.Application.Tasks.DeleteTask;
using orchid_backend_net.Application.Tasks.GetAllTasks;
using orchid_backend_net.Application.Tasks.GetTaskInfor;
using orchid_backend_net.Application.Tasks.UpdateTask;
using orchid_backend_net.Application.Tasks.UpdateTaskStatus;
using System.Net.Mime;

namespace orchid_backend_net.API.Controllers.Task
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController(ISender sender, ILogger<TaskController> logger) : BaseController(sender)
    {
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<GetAllTaskQueryDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<GetAllTaskQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PageResult<GetAllTaskQueryDto>>>> GetAll(
            [FromQuery] int pageNo,
            [FromQuery] int pageSize,
            [FromQuery] string? technicianId,
            [FromQuery] string? researcherId,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new GetAllTaskQuery(pageNo, pageSize, technicianId, researcherId), cancellationToken);
                logger.LogInformation("Received GET request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<PageResult<GetAllTaskQueryDto>>(result));
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, "Error occurred while processing GET request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PageResult<TaskDTO>>>> GetAll(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(new GetTaskInforQuery(id), cancellationToken);
                logger.LogInformation("Received GET request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<TaskDTO>(result));
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, "Error occurred while processing GET request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> Create (
            [FromBody] CreateTaskCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(command, cancellationToken);
                logger.LogInformation("Received POST request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<string>(result));    
            }
            catch (Exception ex) 
            {
                logger.LogInformation(ex, "Error occurred while processing POST request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> Update(
            [FromBody] UpdateTaskCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(command, cancellationToken);
                logger.LogInformation("Received PUT request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, "Error occurred while processing PUT request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// use when technician update task status
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("update-status")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> Update(
            [FromBody] UpdateTaskStatusCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(command, cancellationToken);
                logger.LogInformation("Received PUT request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, "Error occurred while processing PUT request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<TaskDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> Update(
            [FromBody] DeleteTaskCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var result = await sender.Send(command, cancellationToken);
                logger.LogInformation("Received DELETE request at {Time}", DateTime.UtcNow);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, "Error occurred while processing DELETE request at {Time}", DateTime.UtcNow);
                return BadRequest(ex.Message);
            }
        }
    }
}
