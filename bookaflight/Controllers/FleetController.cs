using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using bookaflight.Model;

namespace bookaflight.Controllers
{
    [ApiController]
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

                Response.StatusCode = 200;
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
                .Where(a => a.Id == id)
                .ToList();

            if (Aircraft.Count < 1)
            {
                Response.StatusCode = 404;
            }
            else
            {
                Response.StatusCode = 200;
            }

            return Aircraft;
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
                return $"Successfully removed from database aircraft where id : {id}";
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                Response.StatusCode = 412;
                return $"Not found Aircraft with id {id}";
            }
        }
    }
}

