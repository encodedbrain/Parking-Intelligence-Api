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

            if (validate.searchingforUser(user.email, user.cpf, user.phone))
                return BadRequest("user already exists");

            if (
                !validate.ValidateCredentials(
                    user.email,
                    user.nickname,
                    user.Fullname,
                    user.cpf,
                    user.phone,
                    user.password
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
            if (!validations.ValidateCredentials(user.email, user.password, user))
                return BadRequest("invalid credentials");
            var User = validations.ReturnUser(user.email, user.password);
            if (User == null)
                return BadRequest();
            return Ok(User);
        }
    }
}
