using MediatR;
using orchid_backend_net.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Authentication.Login
{
    public record LoginQuery(string UserID, string Password) : IRequest<LoginDTO>, IQuery
    {
        public string UserID { get; } = UserID;
        public string Password { get; } = Password;
    }
}
