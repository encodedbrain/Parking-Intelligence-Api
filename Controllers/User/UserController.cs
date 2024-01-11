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
            return NotFound("null user or invalid credentials");
        return Ok(userValidation);
    }


    [HttpDelete]
    [Route("delete/user")]
    [Authorize]
    public IActionResult UserDelete([FromBody] LoginSchema prop)
    {
        var user = new UserServices();


        if (string.IsNullOrEmpty(prop.Email) || string.IsNullOrEmpty(prop.Password))
            return NotFound("invalid fields");

        var status = user.Service.Delete(prop).Result;
        if (status is false)
            return BadRequest("error, unable to execute the delete command");

        return Ok();
    }


    [Route("update/user")]
    [HttpPatch]
    [AllowAnonymous]
    public IActionResult UpdateUser([FromBody] UpdatePasswordSchema prop)
    {
        var user = new UserServices();
        var status = user.Service.UpdatePassword(prop);

        if (!status)
        {
            return NotFound("this user does not exist");
        }

        return Ok();
    }

    [HttpPost]
    [Route("download/profile")]
    public IActionResult DownloadPhotoUser([FromBody] DownloadSchema prop)
    {
        UserServices userServices = new UserServices();
        var status = userServices.Service.DownloadPhoto(prop);

        return File(status, "image/png");
    }

    [HttpPatch]
    [Route("update/photo")]
    public IActionResult UpdatePhotoProfile([FromForm] UpdatePhotoProfileSchema prop)
    {
        UserServices userServices = new UserServices();
        var status = userServices.Service.UpdatePhotoProfile(prop).Result;

        if (status is false) return BadRequest();
        return Ok();
    }
}