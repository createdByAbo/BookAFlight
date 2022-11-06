using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using BookAFlight.Entities;
using BookAFlight.Context;
using Microsoft.AspNetCore.Authorization;

namespace BookAFlight.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize(Roles = "Mechanic")]
    [Route("api/[controller]")]
    public class FleetController : ControllerBase
    {

    }
}

