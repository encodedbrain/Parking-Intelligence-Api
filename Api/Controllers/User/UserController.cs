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
    [Route("user/create")]
    [AllowAnonymous]
    public IActionResult CreateUser([FromBody] UserSchema prop)
    {
        var service = new CreateUserServices();

        var status = service.CreateNewUser(prop).Result;

        if (status is false) return BadRequest("to something wrong in one of the fields");

        return Ok(status);
    }

    [HttpPost]
    [Route("login")]
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
}