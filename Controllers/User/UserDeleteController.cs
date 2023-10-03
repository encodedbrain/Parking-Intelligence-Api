using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserDeleteController : ControllerBase
    {
        [HttpDelete]
        [Route("user/delete")]
        [Authorize]
        public async Task<IActionResult> UserDelete([FromBody] UserDeleteSchema user)
        {
            DeleteUserServices validate = new DeleteUserServices();

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return BadRequest("invalid fields");

            var deleteUser = await validate.SearchingForUser(user.Email, user.Password);
            if (!deleteUser)
                return BadRequest("error, unable to execute the delete command");

            return Ok("user successfully deleted");
        }
    }
}
