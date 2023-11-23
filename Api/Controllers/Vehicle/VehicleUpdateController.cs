// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Parking_Intelligence_Api.Schemas.vehicle;
// using Parking_Intelligence_Api.Services.Vehicle;
//
// namespace Parking_Intelligence_Api.Controllers.Vehicle;
//
//
// [ApiController]
// [Route("v1")]
// public class VehicleUpdateController : ControllerBase
// {
//     [HttpPut]
//     [Route("vehicle/update")]
//     [Authorize]
//     public Task<IActionResult> VehicleUpdate([FromBody] UpdateVehicleSchema prop , [FromQuery] string vacancy)
//     {
//         var service = new VehicleUpdateService();
//
//         var status = service.VehicleUpdate(prop,vacancy).Result;
//
//         if (status is false) return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));
//
//         return Task.FromResult<IActionResult>(Ok("vehicle successfully update"));
//     }
// }