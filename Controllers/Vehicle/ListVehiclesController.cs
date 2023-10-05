using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Controllers;

[ApiController]
[Route("v1")]
public class ListVehiclesController : ControllerBase

{
    [HttpGet]
    [Route("user/vehicles")]
    [Authorize]
    public Task<IActionResult> GetVehicles([FromQuery] LoginSchema user)
    {
        var services = new VehiclesUserServices();

        var vehicles = services.GetVehicles(user);

        return Task.FromResult<IActionResult>(Ok(vehicles));
    }
}