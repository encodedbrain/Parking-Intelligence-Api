using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas.User;
using Parking_Intelligence_Api.Services.User;

namespace Parking_Intelligence_Api.Controllers.User;

[ApiController]
[Route("v1")]
public class UserDeleteController : ControllerBase
{
    [HttpDelete]
    [Route("user/delete")]
    [Authorize]
    public async Task<IActionResult> UserDelete([FromBody] LoginSchema prop)
    {
        var service = new DeleteUserServices();

        if (string.IsNullOrEmpty(prop.Email) || string.IsNullOrEmpty(prop.Password))
            return BadRequest("invalid fields");

        var deleteUser = await service.SearchingForUser(prop);
        if (!deleteUser)
            return BadRequest("error, unable to execute the delete command");

        return Ok("user successfully deleted");
    }
}