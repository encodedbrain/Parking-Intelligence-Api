// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Parking_Intelligence_Api.Schemas.User;
// using Parking_Intelligence_Api.Services.User;
//
// namespace Parking_Intelligence_Api.Controllers.User;
//
// [ApiController]
// [Route("v1")]
// public class UserUpdateController : ControllerBase
// {
//     // [Route("user/update")]
//     // [HttpPatch]
//     // [Authorize]
//     // public Task<IActionResult> UpdateUser([FromBody] UpdateSchema prop)
//     // {
//     //     var service = new UpdateUserServices();
//     //     var users = new Models.User();
//     //
//     //     var credentials = service.VerifyCredentials(
//     //         prop.FieldEdit,
//     //         prop.Password,
//     //         prop.Email,
//     //         prop.Value
//     //     );
//     //
//     //     if (!credentials)
//     //         return Task.FromResult<IActionResult>(BadRequest("invalid credentials"));
//     //     if (!service.UpdateAddress(prop.Value, users))
//     //         BadRequest("impossible to update field");
//     //     else
//     //         return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
//     //     if (!service.UpdateAddress(prop.Value, users))
//     //         BadRequest("impossible to update address field");
//     //     else
//     //         return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
//     //
//     //     if (!service.UpdateEmail(prop.Value, users))
//     //         BadRequest("impossible to update email field");
//     //     else
//     //         return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
//     //     if (!service.UpdateNickname(prop.Value, users))
//     //         BadRequest("impossible to update nickname field");
//     //     else
//     //         return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
//     //
//     //     if (!service.UpdatePassword(prop.Value, users))
//     //         BadRequest("impossible to update password field");
//     //     else
//     //         return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
//     //     if (!service.UpdatePhone(prop.Value, users))
//     //         BadRequest("impossible to update phone field");
//     //     else
//     //         return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
//     //     return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
//     // }
// }