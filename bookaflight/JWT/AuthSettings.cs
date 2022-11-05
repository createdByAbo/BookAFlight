using System;
using System.Security.Claims;
using BookAFlight.Entities;

namespace BookAFlight.JWT
{
    public class AuthSettings
    {
        public string? JwtKey { get; set; }
        public int ExpireDays { get; set; }
        public string? JwtIssuer { get; set; }
    }
}

