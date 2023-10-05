using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Controllers.Buy;

[ApiController]
[Route("v1")]
public class BuyController : ControllerBase
{
    [HttpPost]
    [Route("user/buy")]
    [Authorize]
    public Task<IActionResult> Buy([FromBody] BuySchema prop)
    {
        var buy = new BuyServices();

        buy.MakingPurchase(prop);

        if (!buy.ValidateCredentials(prop)) return Task.FromResult<IActionResult>(BadRequest("Unable to make purchase"));
        return Task.FromResult<IActionResult>(Ok("Purchase made successfully, thank you and come back soon"));
    }
}