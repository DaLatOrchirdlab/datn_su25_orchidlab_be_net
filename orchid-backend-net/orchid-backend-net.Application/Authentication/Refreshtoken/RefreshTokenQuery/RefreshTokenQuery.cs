using MediatR;
using orchid_backend_net.Application.Authentication.Login;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Authentication.Refreshtoken.RefreshTokenQuery
{
    public class RefreshTokenQuery(string refreshToken) : IRequest<LoginDTO>
    {
        public string RefreshToken { get; set; } = refreshToken;
    }


    public class RefreshTokenQueryHandler(IUserRepository userRepository) : IRequestHandler<RefreshTokenQuery, LoginDTO>
    {
        public async Task<LoginDTO> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(u => u.RefreshToken.Equals(request.RefreshToken)
            && u.Status == true
            && u.RefreshTokenExpiryTime >= DateTime.UtcNow, cancellationToken);
            string role = "";
            role = user.RoleID switch
            {
                0 => "Account does not have a role",
                1 => "Admin",
                2 => "Researcher",
                3 => "Technician",
            };
            return LoginDTO.Create(user.ID, role, user.RefreshToken);
        }
    }
}
