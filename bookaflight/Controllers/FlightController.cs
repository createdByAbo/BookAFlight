using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using bookaflight.Model;

namespace BookAFlight.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class FlightController : ControllerBase
{
    private readonly devEnvDbContext _context;

    public FlightController(devEnvDbContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public List<Fleet> GetFlights()
    {
        var Flights = _context.Fleets
            .ToList();
        return Flights;
    }

    [HttpGet("{id}")]
    public string GetFlightById(int id)
    {
        return id.ToString();
    }

    [HttpPost()]
    public string CreateFlight()
    {
        return "";
    }

    [HttpDelete("{id}")]
    public string DeleteFlight(int id)
    {
        return id.ToString();  
    }

    [HttpPatch("{id}")]
    public string UpdateFlight(int id)
    {
        return "";
    }
}