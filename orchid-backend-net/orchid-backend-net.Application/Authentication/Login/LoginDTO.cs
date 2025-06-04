using AutoMapper;
using orchid_backend_net.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Application.Authentication.Login
{
    public class LoginDTO : IMapFrom<orchid_backend_net.Domain.Entities.User>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<orchid_backend_net.Domain.Entities.User, LoginDTO>();

        }
        public string Id { get; set; }
        public string RoleID { get; set; }
        public required string RefreshToken { get; set; }
        
        public static LoginDTO Create(string UserID, string Role, string RefreshToken)
        {
            return new LoginDTO
            {
                Id = UserID,
                RoleID = Role,
                RefreshToken = RefreshToken
            };
        }

    }
}
