using BookAFlight.Context;
using BookAFlight.Entities;
using BookAFlight.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookAFlight.Services
{
    public interface IFlightService
    {
        List<Flight> FilterFlights(string startDateMin = "", string startDateMax = "");
    }

    public class FlightService : IFlightService
    {
        private readonly devEnvDbContext _context;

        public FlightService(devEnvDbContext context)
        {
            _context = context;
        }

        public List<Flight> FilterFlights(string startDateMin = "", string startDateMax = "")
        {
            var flights = from Flights in _context.Flights
                where Flights.StartDate >= DateTime.Parse(startDateMin) &&
                      Flights.StartDate <= DateTime.Parse(startDateMax).AddDays(1)
                select Flights;
            return flights.ToList();
        }
    }
}
