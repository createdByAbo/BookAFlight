using System;
using BookAFlight.Context;
using BookAFlight.Entities;

namespace BookAFlight.Services
{
    public interface IRoleService
    {
        string GetRoleByUsername(string username);
    }

    public class RoleService : IRoleService
    {
        private readonly devEnvDbContext _context;
        private readonly IUserService _userService;

        public RoleService(devEnvDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public string GetRoleByUsername(string username)
        {
            var role = from roles in _context.Roles
                       where roles.Id == _userService.GetUserRoleIdByUsername(username)
                       select roles.Name;
                    
            return role.First();
        }
    }
}

