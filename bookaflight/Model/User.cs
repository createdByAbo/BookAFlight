﻿using System;
using System.Collections.Generic;

namespace bookaflight.Model
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; }
        public string SurName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal PeselNumber { get; set; }
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public decimal PhoneNumber { get; set; }
        public bool IsActivated { get; set; }
    }
}
