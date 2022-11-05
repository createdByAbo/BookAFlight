using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using BookAFlight.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace BookAFlight.JWT
{
    public static class Token
    {
        public static string CreateToken(User user, AuthSettings _authSettings)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authSettings.ExpireDays);

            var token = new JwtSecurityToken(_authSettings.JwtIssuer, _authSettings.JwtIssuer, claims, expires: expires, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}

