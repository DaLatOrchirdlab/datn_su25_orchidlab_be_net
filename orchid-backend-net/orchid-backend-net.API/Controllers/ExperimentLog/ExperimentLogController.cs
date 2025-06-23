using MediatR;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;
using orchid_backend_net.Application.Common.Pagination;
using orchid_backend_net.Application.ExperimentLog;
using orchid_backend_net.Application.ExperimentLog.CreateExperimentLog;
using orchid_backend_net.Application.ExperimentLog.DeleteExperimentLog;
using orchid_backend_net.Application.ExperimentLog.GetAllExperimentLog;
using orchid_backend_net.Application.ExperimentLog.GetExperimentLogInfor;
using orchid_backend_net.Application.ExperimentLog.UpdateExperimentLog;
using orchid_backend_net.Application.User;
using orchid_backend_net.Application.User.GetAllUser;
using System.Net.Mime;

namespace orchid_backend_net.API.Controllers.ExperimentLog
{
    [Route("api/experimentlog")]
    [ApiController]
    public class ExperimentLogController(ISender _sender, ILogger<ExperimentLogController> _logger) : BaseController(_sender)
    {
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<ExperimentLogDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<ExperimentLogDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<PageResult<ExperimentLogDTO>>>> GetAllExperimentLog(
            [FromRoute] int pageSize, [FromRoute] int pageNumber, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._sender.Send(new GetAllExperimentLogQuery(pageNumber, pageSize), cancellationToken);
                return Ok(new JsonResponse<PageResult<ExperimentLogDTO>>(result));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<ExperimentLogDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<ExperimentLogDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<ExperimentLogDTO>>> GetExperimentLogInfor(
            [FromQuery] string ID, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._sender.Send(new GetExperimentLogInforQuery(ID), cancellationToken);
                return Ok(new JsonResponse<ExperimentLogDTO>(result));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> UpdateExperimentLog(
            UpdateExperimentLogCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._sender.Send(command, cancellationToken); 
                return Ok( new JsonResponse<string>(result));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<string>>> CreateExperimentLog(
            CreateExperimentLogCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._sender.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(JsonResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<JsonResponse<string>>> DeleteExperimentLog(
            DeleteExperimentLogCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._sender.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
