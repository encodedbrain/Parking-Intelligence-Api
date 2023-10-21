using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas.User;
using Parking_Intelligence_Api.Schemas.vehicle;
using Parking_Intelligence_Api.Services.Vehicle;

namespace Parking_Intelligence_Api.Controllers.Vehicle;

[ApiController]
[Route("v1")]
public class ListVehiclesController : ControllerBase

{
    [HttpGet]
    [Route("user/vehicles")]
    [Authorize]
    public Task<IActionResult> GetVehicles([FromQuery] GetVehicleSchema prop)
    {
        var service = new VehiclesUserServices();
        
        var status = service.GetVehicles(prop);

        if (status is false) return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));
        
        return Task.FromResult<IActionResult>(Ok(status));
    }
}