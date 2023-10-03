using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("user/create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserSchema user)
        {
            CreateUserServices validate = new CreateUserServices();
            if (user == null)
                return BadRequest();

            if (validate.searchingforUser(user.Email, user.Cpf, user.Phone))
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
            LoginServices validations = new LoginServices();
            if (!validations.ValidateCredentials(user.Email, user.Password, user))
                return BadRequest("invalid credentials");
            var User = validations.ReturnUser(user.Email, user.Password);
            if (User == null)
                return BadRequest();
            return Ok(User);
        }
    }
}
