using Microsoft.AspNetCore.Mvc;

using BookAFlight.Services;
using BookAFlight.Models;

namespace BookAFlight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]s")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService; 
        }

        [HttpGet]
        public ActionResult FilterFlights( [FromBody] FlightFliter flightData )
        {
            return Ok(_flightService.FilterFlights(startDate: flightData.StartDate, endDate: flightData.EndDate));
        }
    }
}