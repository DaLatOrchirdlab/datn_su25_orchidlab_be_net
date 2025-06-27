using MediatR;
using orchid_backend_net.Application.Authentication.Refreshtoken.GenerateRefreshToken;
using orchid_backend_net.Domain.Common.Exceptions;
using orchid_backend_net.Domain.IRepositories;

namespace orchid_backend_net.Application.Authentication.Login
{
    internal class LoginQueryHandler(IUserRepository _userRepository, ISender sender) : IRequestHandler<LoginQuery, LoginDTO>
    {
        public async Task<LoginDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(_ => _.Email == request.Email && _.Status == true, cancellationToken) ?? throw new NotFoundException("User not found");
            var isTrue = _userRepository.VerifyPassword(request.Password, user.Password);
            if (!isTrue)
            {
                throw new IncorrectPasswordException("Password is incorrect");
            }
            string Role = "";
            Role = user.RoleID switch
            {
                0 => "Account does not have a role",
                1 => "Admin",
                2 => "Researcher",
                3 => "Technician",
            };
            var refresh = sender.Send(new RefreshTokenCommand(), cancellationToken).Result;
            user.RefreshToken = refresh.Token;
            user.RefreshTokenExpiryTime = refresh.Expired;
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return LoginDTO.Create(user.ID, Role, refresh.Token);
        }
    }
}
