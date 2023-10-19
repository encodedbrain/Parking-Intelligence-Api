using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Schemas.buy;
using Parking_Intelligence_Api.Services.Buy;

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
        var service = new BuyServices();

        var status = service.MakingPurchase(prop);

        if (status is false) return Task.FromResult<IActionResult>(BadRequest("Unable to make purchase"));
        return Task.FromResult<IActionResult>(Ok("Purchase made successfully, thank you and come back soon"));
    }
}