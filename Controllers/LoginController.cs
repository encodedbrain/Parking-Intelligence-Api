using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("user/create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(
            [FromServices] ParkingDB DB,
            [FromBody] UserSchema user
        )
        {
            CreateUserServices validate = new CreateUserServices();
            if (user == null)
                return BadRequest();

            if (validate.searchingforUser(user.email, user.cpf, user.phone))
                return BadRequest("user already exists");

            if (
                !validate.ValidateCredentials(
                    user.email,
                    user.nickname,
                    user.Fullname,
                    user.cpf,
                    user.phone,
                    user.password,
                    user.address
                )
            )
                return BadRequest("to something wrong in one of the fields");

            return Ok(await validate.CreateNewUser(user));
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(
            [FromServices] ParkingDB DB,
            [FromBody] Login user
        )
        {
            LoginServices validations = new LoginServices();

            if (!validations.ValidateCredentials(user.email, user.password, user))
                return BadRequest("invalid credentials");

            if (!validations.FindUser(user))
                return NotFound("user not found");
            return Ok(validations.ReturnUser(user));
        }
    }
}
