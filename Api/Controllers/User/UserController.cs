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
        var service = new CreateUserServices();

        var status = service.CreateNewUser(prop).Result;

        if (status is false) return BadRequest("to something wrong in one of the fields");

        return Ok(status);
    }

    [HttpPost]
    [Route("login/user")]
    [AllowAnonymous]
    public IActionResult LoginUser([FromBody] LoginSchema user)
    {
        var validations = new LoginServices();
        if (!validations.ValidateCredentials(user))
            return BadRequest("invalid credentials");
        var userValidation = validations.ReturnUser(user);
        if (userValidation is false)
            return BadRequest("null user or invalid credentials");
        return Ok(userValidation);
    }
    
    
    [HttpDelete]
    [Route("delete/user")]
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
    
    
    
    [Route("update/user")]
    [HttpPatch]
    [Authorize]
    public Task<IActionResult> UpdateUser([FromBody] UpdateSchema prop)
    {
        var service = new UpdateUserServices();
        var users = new Models.User();

        var credentials = service.VerifyCredentials(
            prop.FieldEdit,
            prop.Password,
            prop.Email,
            prop.Value
        );

        if (!credentials)
            return Task.FromResult<IActionResult>(BadRequest("invalid credentials"));
        if (!service.UpdateAddress(prop.Value, users))
            BadRequest("impossible to update field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
        if (!service.UpdateAddress(prop.Value, users))
            BadRequest("impossible to update address field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));

        if (!service.UpdateEmail(prop.Value, users))
            BadRequest("impossible to update email field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
        if (!service.UpdateNickname(prop.Value, users))
            BadRequest("impossible to update nickname field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));

        if (!service.UpdatePassword(prop.Value, users))
            BadRequest("impossible to update password field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
        if (!service.UpdatePhone(prop.Value, users))
            BadRequest("impossible to update phone field");
        else
            return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
        return Task.FromResult<IActionResult>(Ok("Your profile has been updated successfully"));
    }
}