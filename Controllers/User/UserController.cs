using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Controllers;

[ApiController]
[Route("v1")]
public class UserController : ControllerBase
{
    [HttpPost]
    [Route("user/create")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] UserSchema user)
    {
        var validate = new CreateUserServices();

        if (validate.SearchingforUser(user.Email, user.Cpf, user.Phone))
            return BadRequest("user already exists");

        if (
            !validate.ValidateCredentials(
                user.Email,
                user.Nickname,
                user.Fullname,
                user.Cpf,
                user.Phone,
                user.Password
            )
        )
            return BadRequest("to something wrong in one of the fields");

        return Ok(await validate.CreateNewUser(user));
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
        if (userValidation is null)
            return BadRequest("null user or invalid credentials");
        return Ok(userValidation);
    }
}