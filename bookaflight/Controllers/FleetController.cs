using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BCrypt.Net;

using BookAFlight.Entities;
using BookAFlight.Context;
using BookAFlight.Models.DTOs;
using BookAFlight.Services;

using System;
using BookAFlight.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace BookAFlight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Mechanic")]
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
    }
}

