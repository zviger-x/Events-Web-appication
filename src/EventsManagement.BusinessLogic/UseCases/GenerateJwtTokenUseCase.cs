using EventsManagement.BusinessLogic.DataTransferObjects;
using EventsManagement.BusinessLogic.UseCases.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventsManagement.BusinessLogic.UseCases
{
    internal class GenerateJwtTokenUseCase : IGenerateJwtTokenUseCase
    {
        public string Execute((UserDTO user, string secretKey, string issuer, string audience) request)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.user.Name),
                new Claim(ClaimTypes.Email, request.user.Email),
                new Claim(ClaimTypes.Role, request.user.Role),
                new Claim("Id", request.user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: request.issuer,
                audience: request.audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
