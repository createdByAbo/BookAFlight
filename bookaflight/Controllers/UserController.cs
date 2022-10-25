using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using BookAFlight.Models;
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

        [HttpGet]
        public List<User> GetUsers()
        {
            var users = _context.Users
                .ToList();

            return users;
        }


    }
}