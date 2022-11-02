using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using BookAFlight.Models;
using BookAFlight.Context;

namespace BookAFlight.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]s")]
public class FlightController : ControllerBase
{
    private readonly devEnvDbContext _context;

    public FlightController(devEnvDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public List<Flight> GetFlights()
    {
        var Flights = _context.Flights
            .ToList();

        return Flights;
    }

    [HttpPost()]
    public async Task<string> CreateFlight( [FromForm] Flight flight)
    {
        try
        {
            var FlightInstance = new Flight()
            {
                AircraftId = flight.AircraftId,
                StartCity = flight.StartCity,
                StartAirport = flight.StartAirport,
                StartDate = flight.StartDate,
                EndCity = flight.EndCity,
                EndAirport = flight.EndAirport,
                EndDate = flight.EndDate,
                BeetweenAproche = flight.BeetweenAproche,
                BeetweenAprocheDate = flight.BeetweenAprocheDate,
                FlightCode = flight.FlightCode,
                FirstClassSeatPrice = flight.FirstClassSeatPrice,
                BuisnessClassSeatPrice = flight.BuisnessClassSeatPrice,
                EconomicClassSeatPrice = flight.EconomicClassSeatPrice,
                RegistredBaggagePrice = flight.RegistredBaggagePrice,
                NumberOfMaxPersonsWithRegistredBaggage = flight.NumberOfMaxPersonsWithRegistredBaggage
            }; 

            _context.Add(FlightInstance);
            await _context.SaveChangesAsync();

            Response.StatusCode = 201;
            return $"Successfully added flight from {flight.StartCity} to {flight.EndCity}, {flight.StartDate} -> {flight.EndDate}, FLIGHTCODE : {flight.FlightCode}";
        }
        catch (Exception exc)
        {

            Response.StatusCode = 400;
            return $"Failed adding flight to database {exc.Message}";
        }
    }

    [HttpGet("{id}")]
    public List<Flight> GetFlightById(int id)
    {
        var Flights = _context.Flights
            .Where(dbFlight => dbFlight.Id == id)
            .ToList();

        return Flights;
    }

    [HttpPatch("{id}")]
    public string UpdateFlight(int id)
    {
        return "";
    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteFlight(int id)
    {
        try
        {
            var Flights = new Flight { Id = id };
            _context.Remove(Flights);

            await _context.SaveChangesAsync();

            Response.StatusCode = 200;
            return $"Successfully removed from database Flight where id : {id}";
        }
        catch (DbUpdateConcurrencyException)
        {
            Response.StatusCode = 404;
            return $"Not found Aircraft with id {id}";
        }
    }
}