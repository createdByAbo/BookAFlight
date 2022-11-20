using BookAFlight.Context;
using BookAFlight.Entities;
using BookAFlight.Exceptions;

namespace BookAFlight.Services
{
    public interface IFlightService
    {
        List<Flight> FilterFlights(string startCity = "", string startAirport = "", string startDate = "", string endCity = "", string endAirport = "", string endDate = "");
    }

    public class FlightService : IFlightService
    {
        private readonly devEnvDbContext _context;

        public FlightService(devEnvDbContext context)
        {
            _context = context;
        }

        public List<Flight> FilterFlights(string startCity = "", string startAirport = "", string startDate = "", string endCity = "",
            string endAirport = "", string endDate = "")
        {
            var flight = from flights in _context.Flights
                where flights.StartDateOnly == DateTime.Parse(startDate)
                select flights;
            if (flight.Count() < 1) { throw new NotFoundException("Flight not found"); } 
            return flight.ToList();
        }
    }
}

