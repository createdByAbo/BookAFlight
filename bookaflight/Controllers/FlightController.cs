using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookAFlight.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class FlightController : ControllerBase
{
    [HttpGet()]
    public string GetFlights()
    {
        return "Index";
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