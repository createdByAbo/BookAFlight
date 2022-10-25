using System;
using System.Collections.Generic;

namespace BookAFlight.Models
{
    public partial class PassangerDatum
    {
        public int Id { get; set; }
        public int? UserIdIfRegistred { get; set; }
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; }
        public string SurName { get; set; } = null!;
        public decimal PeselNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PhoneNumber { get; set; }
    }
}
