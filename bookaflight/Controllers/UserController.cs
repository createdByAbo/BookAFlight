using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BCrypt.Net;

using BookAFlight.Entities;
using BookAFlight.Context;

using System;
using BookAFlight.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace BookAFlight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    { 
        private readonly devEnvDbContext _context;
        private readonly AuthSettings _authenticationSettings;

        public UserController(devEnvDbContext context, AuthSettings AuthenticationSettings) 
        {
            _context = context;
            _authenticationSettings = AuthenticationSettings;
        }

        [HttpPost]
        public async Task<string> CreateUser( [FromForm] User user )
        {
            try
            {
                var NewUser = new User()
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PeselNumber = user.PeselNumber,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                };

                _context.Add(NewUser);
                await _context.SaveChangesAsync();

                Response.StatusCode = 201;
                return "Ok";
            }
            catch (Exception exc)
            {
                Response.StatusCode = 400;
                return $"Failed adding user to database {exc}";
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public List<User> GetUsers()        {
            var users = _context.Users
                .ToList();

            return users;
        }

        [HttpGet("{id}")]
        public User GetUsetById(int id)
        {
            try
            {
                var user = _context.Users
                    .Where(dbUser => dbUser.Id == id)
                    .First();

                Response.StatusCode = 200;
                return user;
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new User();
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> RemoveUserById(int id)
        {
            try
            {
                var NewUser = new User() { Id = id };

                _context.Remove(NewUser);
                await _context.SaveChangesAsync();
                Response.StatusCode = 200;
                return $"Successfully removed from database user with id : {id}";
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                Response.StatusCode = 404;
                return $"Not found User with id {id}";
            }
        }

        [HttpPost("authenticate")]
        public string Login( [FromForm] string mail, [FromForm] string password)
        {
            try
            {
                var User = _context.Users
                    .Where(obj => obj.Email == mail)
                    .First();

                if (User.IsActivated != true) { return "user not activated"; }
                if (BCrypt.Net.BCrypt.Verify(password, User.Password) == false ) { return "wrong password"; }

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{User.FirstName} {User.LastName}"),
                    new Claim(ClaimTypes.Email, User.Email),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.DateOfBirth, User.DateOfBirth.ToString()),
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expires = DateTime.Now.AddDays(_authenticationSettings.ExpireDays);

                    var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: credentials);
                    var tokenHandler = new JwtSecurityTokenHandler();

                    return tokenHandler.WriteToken(token);

            }
            catch (Exception exc)
            {
                return $"user not found {exc}";
            }
        }
    }
}