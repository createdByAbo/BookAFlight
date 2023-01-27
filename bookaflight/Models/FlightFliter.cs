namespace BookAFlight.Models
{
    public class FlightFliter
    {
        public string? StartDateMin { get; set; }
        public string? StartDateMax { get; set; }
        public string? StartCity { get; set; }
        public string? StartAirportIcao { get; set; }
        public string? EndDateMin { get; set; }
        public string? EndDateMax { get; set; }
        public string? EndCity { get; set; }
        public string? EndAirportIcao { get; set; }
    }
}