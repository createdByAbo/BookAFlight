using System;
using System.Collections.Generic;

namespace BookAFlight.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; }
        public string Username { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PeselNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public bool IsActivated { get; set; }
        public int RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
