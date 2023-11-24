using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas.User;
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
        var service = new VehiclesUserServices();

        var status = service.GetVehicles(prop);

        if (status is false)
            return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));

        return Task.FromResult<IActionResult>(Ok(status));
    }


    [HttpDelete]
    [Route("delete/vehicle")]
    [Authorize]
    public Task<IActionResult> DeleteOneMoreVehicles([FromBody] DeleteVehicleSchema prop)
    {
        var service = new DeleteVehiclesServices();

        var status = service.DeleteVehiclesService(prop).Result;

        if (status is false)
            return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));

        return Task.FromResult<IActionResult>(Ok("vehicle successfully deleted"));
    }


    [HttpPut]
    [Route("update/vehicle")]
    [Authorize]
    public Task<IActionResult> VehicleUpdate([FromBody] UpdateVehicleSchema prop, [FromQuery] string vacancy)
    {
        var service = new VehicleUpdateService();

        var status = service.VehicleUpdate(prop, vacancy).Result;

        if (status is false)
            return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));

        return Task.FromResult<IActionResult>(Ok("vehicle successfully update"));
    }
}