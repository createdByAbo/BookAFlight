using System;
namespace BookAFlight.Models.DTOs
{
    public class AircraftUpdateDTO
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public byte? NumberOfFirstClassSeats { get; set; }
        public byte? NumberOfBusinessClassSeats { get; set; }
        public byte? NumberOfEconomicClassSeats { get; set; }
        public byte NumberOfServiceSeats { get; set; }
        public string? Registry { get; set; }
    }
}