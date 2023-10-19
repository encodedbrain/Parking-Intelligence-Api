using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services.User;

namespace Parking_Intelligence_Api.Controllers.User;

[ApiController]
[Route("v1")]
public class UserUpdateController : ControllerBase
{
    [Route("user/update")]
    [HttpPatch]
    [Authorize]
    public Task<IActionResult> UpdateUser([FromBody] UpdateSchema user)
    {
        var service = new UpdateUserServices();
        var users = new Models.User();

        var credentials = service.VerifyCredentials(
            user.FieldEdit,
            user.Password,
            user.Email,
            user.Value
        );

        if (!credentials)
            return Task.FromResult<IActionResult>(BadRequest("invalid credentials"));
        if (!service.UpdateAddress(user.Value, users))
            BadRequest("impossible to update field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
        if (!service.UpdateAddress(user.Value, users))
            BadRequest("impossible to update address field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));

        if (!service.UpdateEmail(user.Value, users))
            BadRequest("impossible to update email field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
        if (!service.UpdateNickname(user.Value, users))
            BadRequest("impossible to update nickname field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));

        if (!service.UpdatePassword(user.Value, users))
            BadRequest("impossible to update password field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
        if (!service.UpdatePhone(user.Value, users))
            BadRequest("impossible to update phone field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
        return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
    }
}