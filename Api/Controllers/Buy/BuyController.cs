using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas.buy;
using Parking_Intelligence_Api.Schemas.User;
using Parking_Intelligence_Api.Services.Buy;

namespace Parking_Intelligence_Api.Controllers.Buy;

[ApiController]
[Route("v1")]
public class BuyController : ControllerBase
{
    [HttpPost]
    [Route("create/buy")]
    [Authorize]
    public Task<IActionResult> Buy([FromBody] BuySchema prop)
    {
        var service = new BuyServices();

        var status = service.MakingPurchase(prop);

        if (status is false) return Task.FromResult<IActionResult>(BadRequest("Unable to make purchase"));
        return Task.FromResult<IActionResult>(Ok("Purchase made successfully, thank you and come back soon"));
    }

    [HttpDelete]
    [Route("delete/buy")]
    public Task<IActionResult> BuyDelete([FromBody] UserDeleteSchema prop)
    {
        var service = new BuyDeleteService();

        var status = service.BuyDelete(prop).Result;


        if (status is false)
            return Task.FromResult<IActionResult>(BadRequest("Unable to make purchase"));
        return
            Task.FromResult<IActionResult>(Ok("Purchase made successfully, thank you and come back soon"));
    }

    [HttpGet]
    [Route("get/buys")]
    [Authorize]
    public Task<IActionResult> ListingAllBuys([FromQuery] GetBuySchema prop)
    {
        var service = new BuysUserServices();

        var status = service.ListingPurchases(prop);

        if (status is null) return Task.FromResult<IActionResult>(NotFound("error, unable to execute the get command"));

        return Task.FromResult<IActionResult>(Ok(status));
    }
}