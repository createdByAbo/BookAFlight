using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BCrypt.Net;

using BookAFlight.Entities;
using BookAFlight.Context;
using BookAFlight.Models.DTOs;
using BookAFlight.Services;

using System;
using BookAFlight.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace BookAFlight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    { 
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser( [FromForm] RegisterUserDTO registerDto )
        {
            _userService.RegisterUser(registerDto);
            return Ok();
        }

        [Authorize]
        [HttpDelete("deleteAccount")]
        public ActionResult DeleteAccount( [FromHeader] string Authorization )
        {
            var handler = new JwtSecurityTokenHandler();
            var username = handler.ReadJwtToken(Authorization.Replace("Bearer ", "")).Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            if (_userService.RemoveUserByUsername(username)) { return Ok($"succesfully deleted user with username: {username} from database"); }
            else { return BadRequest(); }
        }

        [HttpPost("login")]
        public ActionResult Login( [FromForm] LoginDTO loginDto )
        {
            if (_userService.UsernameAndPasswordCheck(loginDto)) { return Ok(_userService.CreateToken(loginDto)); }
            else { return BadRequest(); }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult RemoveUserById(int id)
        {
           var user = new User() { Id = id };
           if (_userService.RemoveUserById(user)) { return Ok($"succesfully deleted user with id: {id} from database"); }
           else { return BadRequest(); };
        }
    }
}