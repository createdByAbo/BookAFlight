using System;
namespace BookAFlight.Models.DTOs
{
    public class RegisterUserDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PeselNumber { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}

