using AutoMapper;
using orchid_backend_net.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Authentication.Login
{
    public static class LoginDTOMappingExstensions
    {
        public static LoginDTO MapToLoginDTO(this orchid_backend_net.Domain.Entities.User user, IMapper mapper)
        => mapper.Map<LoginDTO>(user);
    }
}
