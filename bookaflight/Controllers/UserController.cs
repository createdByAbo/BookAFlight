using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BCrypt.Net;

using BookAFlight.Entities;
using BookAFlight.Context;

using System;

namespace BookAFlight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly devEnvDbContext _context;

        public UserController(devEnvDbContext context) 
        {
            _context = context;
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
                    SurName = user.SurName,
                    Email = user.Email,
                    PeselNumber = user.PeselNumber,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    IsActivated = "0"
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
        public List<User> GetUsers()
        {
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
    }
}