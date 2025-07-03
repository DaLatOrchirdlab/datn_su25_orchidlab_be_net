using MediatR;
using orchid_backend_net.Application.Authentication.Login;
using orchid_backend_net.Application.Authentication.Refreshtoken.GenerateRefreshToken;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Authentication.Refreshtoken.RefreshTokenQuery
{
    public class RefreshTokenQuery(string refreshToken) : IRequest<LoginDTO>
    {
        public string RefreshToken { get; set; } = refreshToken;
    }


    internal class RefreshTokenQueryHandler(IUserRepository userRepository, ICacheService cacheService,
        ICurrentUserService currentUserService, ISender sender) : IRequestHandler<RefreshTokenQuery, LoginDTO>
    {
        public async Task<LoginDTO> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshTokenKey = $"auth:refresh_token_{currentUserService.UserId}";
            // Check if the refresh token exists in the cache
            var cachedToken = await cacheService.GetAsync(refreshTokenKey);
            Users? user = null;
            if (!cachedToken.ToLower().Equals(request.RefreshToken.ToLower()))
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }
            if (!string.IsNullOrEmpty(cachedToken))
            {
                user = await userRepository.FindAsync(u => u.ID.ToLower().Equals(currentUserService.UserId.ToLower())
                    && u.Status == true
                    && u.RefreshTokenExpiryTime >= DateTime.UtcNow, cancellationToken);
            }
            else if (string.IsNullOrEmpty(cachedToken))
            {
                user = await userRepository.FindAsync(u => u.RefreshToken.Equals(request.RefreshToken)
                        && u.Status == true
                        && u.RefreshTokenExpiryTime >= DateTime.UtcNow, cancellationToken);
            }
            string role = "";
            role = user.RoleID switch
            {
                0 => "Account does not have a role",
                1 => "Admin",
                2 => "Researcher",
                3 => "Technician",
            };

            var refresh = await sender.Send(new RefreshTokenCommand(user.ID), cancellationToken);
            user.RefreshToken = refresh.Token;
            user.RefreshTokenExpiryTime = refresh.Expired;
            await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return LoginDTO.Create(user.ID, role, user.RefreshToken, user.Name);
        }
    }
}
