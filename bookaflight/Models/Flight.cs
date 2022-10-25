using System;
using System.Collections.Generic;

namespace BookAFlight.Models
{
    public partial class Flight
    {
        public int Id { get; set; }
        public int AircraftId { get; set; }
        public string StartCity { get; set; } = null!;
        public string StartAirport { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public string EndCity { get; set; } = null!;
        public string EndAirport { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public string? BeetweenAproche { get; set; }
        public DateTime? BeetweenAprocheDate { get; set; }
        public string FlightCode { get; set; } = null!;
        public decimal? FirstClassSeatPrice { get; set; }
        public decimal? BuisnessClassSeatPrice { get; set; }
        public decimal? EconomicClassSeatPrice { get; set; }
        public decimal RegistredBaggagePrice { get; set; }
        public short NumberOfMaxPersonsWithRegistredBaggage { get; set; }

        public virtual Fleet Aircraft { get; set; } = null!;
    }
}
