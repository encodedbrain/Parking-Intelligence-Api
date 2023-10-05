using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Controllers.Buy;

[ApiController]
[Route("v1")]
public class BuyUserController : ControllerBase
{
    [HttpGet]
    [Route("user/buys")]
    [Authorize]
    public async Task<IActionResult> ListingAllBuys([FromQuery] LoginSchema prop)
    {
        var services = new BuysUserServices();

        var buys = services.ListingPurchases(prop);

        if (buys is null) return NotFound("error, unable to execute the delete command");

        return Ok(buys);
    }
}