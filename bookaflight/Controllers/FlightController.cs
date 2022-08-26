using Microsoft.AspNetCore.Mvc;

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
}