using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services.Vehicle;

namespace Parking_Intelligence_Api.Controllers.Vehicle;

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
        
        var status = services.GetVehicles(user);

        if (status is false) return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));
        
        return Task.FromResult<IActionResult>(Ok(status));
    }
}