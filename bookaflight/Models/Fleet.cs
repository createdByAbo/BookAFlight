using System;
using System.Collections.Generic;

namespace BookAFlight.Models
{
    public partial class Fleet
    {
        public Fleet()
        {
            Flights = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public byte? NumberOfFirstClassSeats { get; set; }
        public byte? NumberOfBusinessClassSeats { get; set; }
        public byte? NumberOfEconomicClassSeats { get; set; }
        public byte NumberOfServiceSeats { get; set; }
        public string Registry { get; set; } = null!;

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
