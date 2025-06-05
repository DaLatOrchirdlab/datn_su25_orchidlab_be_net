using Microsoft.AspNetCore.Authorization;
using orchid_backend_net.Application.Common.Interfaces;
using IdentityModel;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using orchid_backend_net.Application.User;

namespace orchid_backend_net.API.Service
{
    public class JwtService
    {
        public class Token
        {
            public required string AccessToken { get; set; }
            public required string RefreshToken { get; set; }
            public UserDTO? UserDTO { get; set; } = null;
        }
        public Token CreateToken(string ID, string roles, string refreshToken, string? RestaurantID)
        {
            var claims = new List<Claim>
            {

                new(JwtRegisteredClaimNames.Sub, ID.ToString()),
                new(ClaimTypes.Role, roles.ToString()),
                new("RoleName",roles.ToString())
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Orchid Lab @PI 123abc456 pass ddoo ans nha troiwf oiwi"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: "https://orchidlabsystem.azurewebsites.net/",
                 audience: "api",
                claims: claims,
                expires: DateTime.Now.AddYears(1),
                signingCredentials: creds);
            var re = new Token
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
            return re;
        }
    }
}
