using BookAFlight.Entities;
using BookAFlight.Exceptions;
using BookAFlight.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using BookAFlight.Services;

using Microsoft.AspNetCore.Authorization;

namespace BookAFlight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    //[Authorize(Roles = "Mechanic")]
    [Route("api/[controller]")]
    public class FleetController : ControllerBase
    {
        private readonly IFleetService _fleetService;

        public FleetController(IFleetService fleetService)
        {
            _fleetService = fleetService;
        }

        [HttpGet]
        public ActionResult GetAllAircrafts()
        {
            return Ok(_fleetService.GetAllAircrafts());
        }

        [HttpGet("id/{id}")]
        public ActionResult GetAircraftById(int id)
        {
            return Ok(_fleetService.GetAircraftById(id));
        }

        [HttpDelete("id/{id}")]
        public ActionResult DeleteAircraftById(int id)
        {
            try
            {
                var aircraft = new Fleet() { Id = id };
                _fleetService.DeleteAircraftById(aircraft);
                return Ok($"Successfully removed aircraft with id {aircraft.Id}");
            }
            catch (DbConflictException exc)
            {
                return BadRequest(exc.Message);
            }
            catch (NotFoundException exc)
            {
                return NotFound(exc.Message);
            }
            catch (ApplicationException)
            {
                return Problem();
            }
        }

        [HttpGet("registration/{registration}")]
        public ActionResult GetAircraftByRegistration(string registration)
        {
            return Ok(_fleetService.GetAircraftByRegistration(registration));
        }

        [HttpGet("ids")]
        public ActionResult GetAircraftsByIds( [FromForm] string ids)
        {
            return Ok(_fleetService.GetAircraftsByIds(JsonConvert.DeserializeObject<List<int>>(ids).ToList()));
        }

        [HttpGet("registrations")]
        public ActionResult GetAircraftsByRegistrations([FromForm] string registrations)
        {
            return Ok(_fleetService.GetAircraftsByRegistrations(JsonConvert.DeserializeObject<List<string>>(registrations).ToList()));
        }

        [HttpPut("replaceAircraftData")]
        public ActionResult ReplaceAircraftDataById([FromForm] AircraftUpdateDTO newData)
        {
            try
            {
                return Ok(_fleetService.ReplaceAircraftData(newData));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

