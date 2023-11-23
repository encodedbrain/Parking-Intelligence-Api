// using Microsoft.AspNetCore.Mvc;
// using Parking_Intelligence_Api.Schemas.User;
// using Parking_Intelligence_Api.Services.Buy;
//
// namespace Parking_Intelligence_Api.Controllers.Buy;
//
//
// [ApiController]
// [Route("v1")]
// public class BuyDeleteController : ControllerBase
// {
//     [HttpDelete]
//     [Route("buy/delete")]
//     public Task<IActionResult> BuyDelete([FromBody] UserDeleteSchema prop)
//     {
//         var service = new BuyDeleteService();
//
//         var status = service.BuyDelete(prop).Result;
//
//
//         if (status is false)
//             return Task.FromResult<IActionResult>(BadRequest("Unable to make purchase"));
//         return 
//             Task.FromResult<IActionResult>(Ok("Purchase made successfully, thank you and come back soon"));
//     }
// }