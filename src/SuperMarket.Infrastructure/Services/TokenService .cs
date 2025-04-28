using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuperMarket.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;
        private readonly string _audience;
        public TokenService(IConfiguration config)
        {
            var secret = "supermarket-secret-key-should-be-long-enough"; //should be loaded from aws secret manager
            _issuer = "SuperMarketAPI";
            _audience = "SuperMarketClient";

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }
        public string GenerateToken(string username, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _issuer,
               audience: _audience,
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(30),
               signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);//where signing actually happens
        }
    }
}
