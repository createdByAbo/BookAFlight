using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using BookAFlight.Entities;
using BookAFlight.Context;

namespace BookAFlight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FleetController : ControllerBase
    {
        private readonly devEnvDbContext _context;

        public FleetController(devEnvDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Fleet> GetFleet()
        {
            var Fleet = _context.Fleets
                .ToList();

            return Fleet;
        }

        [HttpPost]
        public async Task<string> AddAircraft( [FromForm] Fleet aircraft )
        {
            try
            {
                var Aircraft = new Fleet()
                {
                    Brand = aircraft.Brand,
                    Model = aircraft.Model,
                    NumberOfFirstClassSeats = aircraft.NumberOfFirstClassSeats,
                    NumberOfBusinessClassSeats = aircraft.NumberOfBusinessClassSeats,
                    NumberOfEconomicClassSeats = aircraft.NumberOfEconomicClassSeats,
                    NumberOfServiceSeats = aircraft.NumberOfServiceSeats,
                    Registry = aircraft.Registry
                };

                _context.Add(Aircraft);
                await _context.SaveChangesAsync();

                Response.StatusCode = 201;
                return $"Successfully added aircraft with registry : {aircraft.Registry}:";
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return $"Failed adding aircraft to database";
            }
        }

        [HttpGet("{id}")]
        public List<Fleet> GetAircraft(int id)
        {
            var Aircraft = _context.Fleets
                .Where(dbAircraft => dbAircraft.Id == id)
                .ToList();

            Response.StatusCode = (Aircraft.Count < 1) ? 404 : 200;
            return Aircraft;
        }

        [HttpPut("{id}")]
        public async Task<string> UpdateAircraftData(
            int id,
            [FromForm] Fleet aircraft
        )
        {
            try
            {
                aircraft.Id = id;
                _context.Update(aircraft);
                await _context.SaveChangesAsync();
                return $"successfully replaced aircraft data - aircraft id :{id}";
            }
            catch (Exception)
            {
                return "failed replacing aircraft data";
            }
        }
        
        [HttpPatch("{id}")]
        public async Task<string> UpdateAircraft(
            int id,
            [FromForm] string? brand,
            [FromForm] string? model,
            [FromForm] byte? numberOfFirstClassSeats,
            [FromForm] byte? numberOfBusinessClassSeats,
            [FromForm] byte? numberOfEconomicClassSeats,
            [FromForm] byte? numberOfServiceSeats,
            [FromForm] string? registry
        )
        {
            try
            {
                var Aircraft = _context.Fleets
                    .Where(dbAircraft => dbAircraft.Id == id)
                    .ToList();

                Aircraft[0].Brand = (brand == null) ? Aircraft[0].Brand : brand;
                Aircraft[0].Model = (model == null) ? Aircraft[0].Model : model;
                Aircraft[0].NumberOfFirstClassSeats = (numberOfFirstClassSeats == null) ? Aircraft[0].NumberOfFirstClassSeats : numberOfFirstClassSeats;
                Aircraft[0].NumberOfBusinessClassSeats = (numberOfBusinessClassSeats == null) ? Aircraft[0].NumberOfBusinessClassSeats : numberOfBusinessClassSeats;
                Aircraft[0].NumberOfEconomicClassSeats = (numberOfEconomicClassSeats == null) ? Aircraft[0].NumberOfEconomicClassSeats : numberOfEconomicClassSeats;
                Aircraft[0].NumberOfServiceSeats = (byte)((numberOfServiceSeats == null) ? Aircraft[0].NumberOfServiceSeats : numberOfServiceSeats);
                Aircraft[0].Registry = (registry == null) ? Aircraft[0].Registry : registry;

                _context.Update(Aircraft[0]);
                await _context.SaveChangesAsync();


                Response.StatusCode = 200;
                return $"Successfully updated data for aircraft with id : {id}";
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return $"failed updating aircraft ID: {id}";
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteAircraft(int id)
        {
            try
            {
                var Aircraft = new Fleet()
                {
                    Id = id
                };

                _context.Remove(Aircraft);
                await _context.SaveChangesAsync();

                Response.StatusCode = 200;
                return $"Successfully removed from database aircraft with id : {id}";
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                Response.StatusCode = 404;
                return $"Not found Aircraft with id {id}";
            }
        }
    }
}

