using Microsoft.AspNetCore.Mvc;

namespace BookAFlight.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IndexController : ControllerBase
{
    [HttpGet(Name = "GetIndex")]
    public string Get()
    {
        return "Index"; 
    }
}