using orchid_backend_net.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Application.Authentication.Refrestoken.GenerateRefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshToken>, ICommand
    {
    }
}
