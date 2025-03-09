using HibaVonal.DataContext.Entities;
using HibaVonal.DataContext;

using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HibaVonal.DataContext.Dtos;

namespace HibaVonal.Services.Services
{
    public class TokenHandlerService
    {
        private readonly SQL _context;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly byte[] _jwtKey;
        public TokenHandlerService(SQL context, byte[] jwtKey, string issuer, string audience)
        {
            _context = context;
            _jwtKey = jwtKey;
            _issuer = issuer;
            _audience = audience;
        }

        public AccessTokenDto GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name)
            };

            user.Roles = _context.UserRole.Where(a => a.UserId == user.Id).ToList();
            user.Roles.Select(a => a.RoleId).ToList().ForEach(a =>
            {
                claims.Add(new Claim(ClaimTypes.Role, a.ToString()));
            });

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _audience,
                Issuer = _issuer,
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AccessTokenDto()
            {
                Token = tokenHandler.WriteToken(token),
            };
        }
    }
}
