using System.Data.SqlTypes;
using BookAFlight.Context;
using BookAFlight.Entities;

namespace BookAFlight.Services
{
    public interface IFlightService
    {
        List<Flight> FilterFlights(string startDateMin = "", string startDateMax = "", string startCity= "", string endCity = "");
    }

    public class FlightService : IFlightService
    {
        private readonly devEnvDbContext _context;

        public FlightService(devEnvDbContext context)
        {
            _context = context;
        }

        public List<Flight> FilterFlights(string startDateMin = "", string startDateMax = "", string startCity= "", string endCity = "")
        {
            var flights = from flight in _context.Flights
                where flight.StartDate >= (startDateMin != null ? DateTime.Parse(startDateMin) : DateTime.Parse(SqlDateTime.MinValue.ToString())) &&
                      flight.StartDate <= (startDateMax != null ? DateTime.Parse(startDateMax).AddDays(1) : DateTime.Parse(SqlDateTime.MaxValue.ToString())) &&
                      flight.StartCity == startCity &&
                      flight.EndCity == endCity
                select flight;
            return flights.ToList();
        }
    }
}
 