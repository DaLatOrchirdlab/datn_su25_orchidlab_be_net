using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.Authentication.Login
{
    public record LoginQuery(string email, string password) : IRequest<LoginDTO>, IQuery
    {
        public string Email { get; } = email;
        public string Password { get; } = password;
    }
}
