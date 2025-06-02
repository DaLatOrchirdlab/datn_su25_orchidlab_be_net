using MediatR.Pipeline;
using orchid_backend_net.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest>(ILogger<TRequest> logger, ICurrentUserService currentUserService) : IRequestPreProcessor<TRequest>
         where TRequest : notnull
    {
        private readonly ILogger _logger = logger;

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = currentUserService.UserId ?? string.Empty;
            var userName = currentUserService.UserName ?? string.Empty;

            _logger.LogInformation("TestCA9 Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
            return Task.CompletedTask;
        }
    }
}
