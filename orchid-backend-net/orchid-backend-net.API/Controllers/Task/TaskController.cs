using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using orchid_backend_net.API.Controllers.ResponseTypes;

namespace orchid_backend_net.API.Controllers.Task
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController(ISender sender, ILogger<TaskController> logger) : BaseController(sender)
    {
    }
}
