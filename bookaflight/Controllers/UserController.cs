using Microsoft.AspNetCore.Mvc;

using BookAFlight.Entities;
using BookAFlight.Models.DTOs;
using BookAFlight.Models;
using BookAFlight.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

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
        public ActionResult DeleteAccount( [FromHeader] string authorization )
        {
            var handler = new JwtSecurityTokenHandler();
            var username = handler.ReadJwtToken(authorization.Replace("Bearer ", "")).Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            if (_userService.RemoveUserByUsername(username)) { return Ok($"succesfully deleted user with username: {username} from database"); }
            else { return BadRequest(); }
        }

        [HttpPost("login")]
        public ActionResult Login( [FromForm] LoginDTO loginDto )
        {
            try
            {
                _userService.UsernameAndPasswordCheck(loginDto);
                JwtReturnModel returnData = new JwtReturnModel(_userService.CreateToken(loginDto)[0], _userService.CreateToken(loginDto)[1]);
                return Ok(JsonConvert.SerializeObject(returnData));
            }
            catch (Exceptions.NotFoundException exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult RemoveUserById(int id)
        {
           var user = new User() { Id = id };
           if (_userService.RemoveUserById(user)) { return Ok($"succesfully deleted user with id: {id} from database"); }
           else { return BadRequest('s'); };
        }
    }
}