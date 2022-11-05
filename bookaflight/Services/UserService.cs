using System;

using BookAFlight.Context;
using BookAFlight.JWT;
using BookAFlight.Models.DTOs;
using BookAFlight.Entities;

using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookAFlight.Services
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDTO registerDto);
        public string CreateToken(LoginDTO loginDto);
        int GetUserRoleIdByUsername(string username);
    }

    public class UserService : IUserService
    {
        private readonly devEnvDbContext _context;
        private readonly AuthSettings _authenticationSettings;
        private readonly IRoleService _roleService;

        public UserService(devEnvDbContext context, AuthSettings authenticationSettings, IRoleService roleService)
        {
            _context = context;
            _roleService = roleService;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(RegisterUserDTO registerDto)
        {
            var NewUser = new User()
            {
                Email = registerDto.Email,
                DateOfBirth = registerDto.DateOfBirth,
                FirstName = registerDto.FirstName,
                SecondName = registerDto.SecondName,
                LastName = registerDto.LastName,
                Username = registerDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                PeselNumber = registerDto.PeselNumber,
                PhoneNumber = registerDto.PhoneNumber,
            };

            _context.Users.Add(NewUser);
            _context.SaveChanges();
        }

        public int GetUserRoleIdByUsername(string username)
        {
            var id = from user in _context.Users
                     where user.Username == username
                     select user.RoleId;
            return id.First();
        }

        public string CreateToken(LoginDTO loginDto)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginDto.Username.ToString()),
                new Claim(ClaimTypes.Role, _roleService.GetRoleByUsername(loginDto.Username)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.ExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}

