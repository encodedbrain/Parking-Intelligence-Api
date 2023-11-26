using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas.vehicle;
using Parking_Intelligence_Api.Services.Vehicle;

namespace Parking_Intelligence_Api.Controllers.Vehicle;

[ApiController]
[Route("v1")]
public class VehiclesController : ControllerBase

{
    [HttpGet]
    [Route("get/vehicles")]
    [Authorize]
    public Task<IActionResult> GetVehicles([FromQuery] GetVehicleSchema prop)
    {
        var vehicle = new VehicleServices();

        var status = vehicle.Services.GetVehicles(prop);

        if (status.Length < 1)
            return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));

        return Task.FromResult<IActionResult>(Ok(status));
    }


    [HttpDelete]
    [Route("delete/vehicle")]
    [Authorize]
    public Task<IActionResult> DeleteOneMoreVehicles([FromBody] DeleteVehicleSchema prop)
    {
        var vehicle = new VehicleServices();

        var status = vehicle.Services.DeleteVehicle(prop);
    
        if (status is false)
            return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));
    
        return Task.FromResult<IActionResult>(Ok("vehicle successfully deleted"));
    }
    
    
    [HttpPut]
    [Route("update/vehicle")]
    [Authorize]
    public Task<IActionResult> VehicleUpdate([FromBody] UpdateVehicleSchema prop, [FromQuery] string vacancy)
    {
        var vehicle = new VehicleServices();
    
        var status = vehicle.Services.UpdateVehicle(prop, vacancy);
    
        if (status is false)
            return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));
    
        return Task.FromResult<IActionResult>(Ok("vehicle successfully update"));
    }
}