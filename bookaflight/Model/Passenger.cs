using System;
using System.Collections.Generic;

namespace BookAFlight.Model
{
    public partial class Passenger
    {
        public int Id { get; set; }
        public int ReservationCreatorId { get; set; }
        public int FlightId { get; set; }
        public int PassengerRegistryId { get; set; }
        public string SeatType { get; set; } = null!;
        public bool RegistredBaggage { get; set; }
    }
}
