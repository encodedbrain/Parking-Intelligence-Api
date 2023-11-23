// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Parking_Intelligence_Api.Schemas.vehicle;
// using Parking_Intelligence_Api.Services.Vehicle;
//
// namespace Parking_Intelligence_Api.Controllers.Vehicle;
//
// [ApiController]
// [Route("v1")]
// public class DeleteVehiclesController : ControllerBase
// {
//     [HttpDelete]
//     [Route("user/vehicle/delete")]
//     [Authorize]
//     public Task<IActionResult> DeleteOneMoreVehicles([FromBody] DeleteVehicleSchema prop)
//     {
//         var service = new DeleteVehiclesServices();
//
//         var status = service.DeleteVehiclesService(prop).Result;
//
//         if (status is false)
//             return Task.FromResult<IActionResult>(NotFound("something wrong, maybe this vehicle doesn't exist"));
//
//         return Task.FromResult<IActionResult>(Ok("vehicle successfully deleted"));
//     }
// }