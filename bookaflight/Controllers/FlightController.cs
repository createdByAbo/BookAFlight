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

        [HttpPost]
        public ActionResult FilterFlights( [FromForm] FlightFliter flightData )
        {
            try
            {
                var res = _flightService.FilterFlights(startDateMin: flightData.StartDateMin,
                    startDateMax: flightData.StartDateMax, startCity: flightData.StartCity, endCity: flightData.EndCity);
                if (res.Count() == 0)
                {   
                    return NotFound();
                }

                return Ok(res);
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