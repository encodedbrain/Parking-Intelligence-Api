using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas.User;
using Parking_Intelligence_Api.Services.User;

namespace Parking_Intelligence_Api.Controllers.User;

[ApiController]
[Route("v1")]
public class UserController : ControllerBase
{
    [HttpPost]
    [Route("create/user")]
    [AllowAnonymous]
    public IActionResult CreateUser([FromBody] UserSchema prop)
    {
        var user = new UserServices();

        var status = user.Service.Create(prop).Result;

        if (status is false) return BadRequest("to something wrong in one of the fields");

        return Ok(status);
    }

    [HttpPost]
    [Route("login/user")]
    [AllowAnonymous]
    public IActionResult LoginUser([FromBody] LoginSchema prop)
    {
        var user = new UserServices();
        if (!user.ValidateCredentials(prop))
            return BadRequest("invalid credentials");
        var userValidation = user.Service.Login(prop);
        if (userValidation is false)
            return BadRequest("null user or invalid credentials");
        return Ok(userValidation);
    }


    [HttpDelete]
    [Route("delete/user")]
    [Authorize]
    public async Task<IActionResult> UserDelete([FromBody] LoginSchema prop)
    {
        var user = new UserServices();


        if (string.IsNullOrEmpty(prop.Email) || string.IsNullOrEmpty(prop.Password))
            return BadRequest("invalid fields");

        var status = await user.Service.Delete(prop);
        if (status is false)
            return BadRequest("error, unable to execute the delete command");

        return Ok("user successfully deleted");
    }


    [Route("update/user")]
    [HttpPatch]
    [Authorize]
    public Task<IActionResult> UpdateUser([FromBody] UpdateSchema prop)
    {
        var user = new UserServices();

        if (!user.Service.Update(prop))
        {
            return Task.FromResult<IActionResult>(BadRequest("this user does not exist"));
        }

        return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
    }
}