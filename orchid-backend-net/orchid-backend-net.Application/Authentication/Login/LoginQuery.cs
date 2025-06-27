using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.Authentication.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<LoginDTO>, IQuery
    {
        public string Email { get; set; } = Email;
        public string Password { get; set; } = Password;
    }
}
