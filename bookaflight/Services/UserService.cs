using BookAFlight.Context;
using BookAFlight.JWT;
using BookAFlight.Models.DTOs;
using BookAFlight.Entities;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookAFlight.Services
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDTO registerDto);
        string CreateToken(LoginDTO loginDto);
        string GetUserRoleByUsername(string username);
        bool UsernameAndPasswordCheck(LoginDTO loginDto);
        bool RemoveUserById(User user);
        bool RemoveUserByUsername(string username);
    }

    public class UserService : IUserService
    {
        private readonly devEnvDbContext _context;
        private readonly AuthSettings _authenticationSettings;

        public UserService(devEnvDbContext context, AuthSettings authenticationSettings)
        {
            _context = context;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(RegisterUserDTO registerDto)
        {
            var newUser = new User()
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

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public bool UsernameAndPasswordCheck(LoginDTO loginDto)
        {
            var passwordHash = from user in _context.Users
                       where user.IsActivated == true
                       where user.Username == loginDto.Username
                       select user.Password;

            if (passwordHash.Count() != 1) { throw new Exceptions.NotFoundException("User not found, or not activated"); };
            return BCrypt.Net.BCrypt.Verify(loginDto.Password, passwordHash.First());
        }

        public string GetUserRoleByUsername(string username)
        {
            var roleId = from user in _context.Users
                     where user.Username == username
                     select user.Role;
            return roleId.First().Name;
        }

        public bool RemoveUserById(User user)
        {
            try
            {
                _context.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GetIdByUsername(string username)
        {
            var id = from user in _context.Users
                     where user.Username == username
                     select user.Id;
            return id.FirstOrDefault();
        }

        public bool RemoveUserByUsername(string username)
        {
            try
            {
                var user = new User() { Id = GetIdByUsername(username) };
                _context.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string CreateToken(LoginDTO loginDto)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginDto.Username),
                new Claim(ClaimTypes.Role, GetUserRoleByUsername(loginDto.Username)),
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

