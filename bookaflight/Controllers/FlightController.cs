using BookAFlight.Exceptions;
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
        public ActionResult FilterFlights( [FromForm] FlightFliter flightData )
        {
            try
            {
                return Ok(_flightService.FilterFlights(startDateMin: flightData.StartDate, startDateMax: flightData.EndDate));
            }
            catch (NotFoundException exc)
            {
                return NotFound(exc.Message);
            }
            catch (InvalidOperationException exc)
            {
                switch (exc.Message)
                {
                    
                    case "An exception was thrown while attempting to evaluate a LINQ query parameter expression. See the inner exception for more information. To show additional information call 'DbContextOptionsBuilder.EnableSensitiveDataLogging'.":
                        return BadRequest("wrong data type in form");
                    default:
                        return BadRequest("Unknown client error");
                }
            }
        }
    }
}