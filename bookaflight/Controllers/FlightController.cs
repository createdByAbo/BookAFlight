using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using BookAFlight.Entities;
using BookAFlight.Context;

namespace BookAFlight.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]s")]
public class FlightController : ControllerBase
{
    
}