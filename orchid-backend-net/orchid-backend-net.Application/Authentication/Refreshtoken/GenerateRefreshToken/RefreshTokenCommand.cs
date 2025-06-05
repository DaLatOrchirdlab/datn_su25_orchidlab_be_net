using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using orchid_backend_net.Domain.Entities.Base;

namespace orchid_backend_net.Application.Authentication.Refreshtoken.GenerateRefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshToken>, ICommand
    {
    }
}
