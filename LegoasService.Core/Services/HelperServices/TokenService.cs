using LegoasService.Core.CustomEntities.Options;
using LegoasService.Core.DAOs;
using LegoasService.Core.DTOs;
using LegoasService.Core.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LegoasService.Core.Services.HelperServices
{
    public class TokenService : ITokenService
    {
        private readonly TokenOptions _options;
        public TokenService(IOptions<TokenOptions> options)
        {
            _options = options.Value;
        }

        public LoginResponse GenerateTokenOfficer(Officer officer)
        {
            //header
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCrendetials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCrendetials);

            //Claims
            var claims = new[]
            {
                new Claim("Email", officer.Email),
                new Claim("Id", officer.Id.ToString())
            };

            var current = DateTime.UtcNow;

            //Payload
            var paylaod = new JwtPayload
            (
                _options.Issuer,
                _options.Audience,
                claims,
                current,
               current.AddDays(1)
            );

            //Token
            var token = new JwtSecurityToken(header, paylaod);

            var dataToken = new JwtSecurityTokenHandler().WriteToken(token);

            //Add data token
            return new LoginResponse
            {
                Token = dataToken,
                Expiry = current.AddDays(1)
            };
        }
    }
}
