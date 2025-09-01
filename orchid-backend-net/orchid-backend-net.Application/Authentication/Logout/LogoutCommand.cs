﻿using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.Authentication.Logout
{
    public class LogoutCommand(string refreshToken) : IRequest, ICommand
    {
        public string refreshToken { get; } = refreshToken;
    }

    internal class  LogoutCommandHandler(ICacheService cacheService) : IRequestHandler<LogoutCommand>
    {
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cacheKey = $"auth:refresh_token:{request.refreshToken.Trim().ToLowerInvariant()}";
                await cacheService.RemoveAsync(cacheKey);
            }
            catch(Exception ex)
            {
                //log the exception
                //but do not throw it, because we want to logout the user anyway
                //we don't want to give away information about whether the token was valid or not
                //just log it and move on
                throw new ArgumentException("An error occurred while logging out.", ex);
            }
        }
    }
}
