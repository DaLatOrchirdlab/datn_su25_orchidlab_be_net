using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities.Base;
using System.Security.Cryptography;

namespace orchid_backend_net.Application.Authentication.Refreshtoken.GenerateRefreshToken
{
    public class RefreshTokenCommand(string userID) : IRequest<RefreshToken>, ICommand
    {
        public string UserID { get; set; } = userID;
    }
    internal class RefreshTokenCommandHandler(ICacheService cacheService) : IRequestHandler<RefreshTokenCommand, RefreshToken>
    {
        public async Task<RefreshToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string token = GenerateRefreshToken();
            var refreshToken = new RefreshToken
            {
                Token = token,
                Expired = DateTime.UtcNow.AddDays(7)
            };


            var expiryDateInRedis = refreshToken.Expired - DateTime.UtcNow;
            if (expiryDateInRedis < TimeSpan.Zero)
            {
                throw new InvalidOperationException("Refresh token has already expired.");
            }
            var cacheKey = $"auth:refresh_token_{request.UserID}";
            await cacheService.SetAsync(cacheKey, refreshToken.Token, expiryDateInRedis);

            return refreshToken;
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
