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
        var buy = new BuyServices();
    
        var status = buy.Service.Purchase(prop);
    
        if (status is false) return Task.FromResult<IActionResult>(BadRequest("Unable to make purchase"));
        return Task.FromResult<IActionResult>(Ok("Purchase made successfully, thank you and come back soon"));
    }

    [HttpDelete]
    [Route("delete/buy")]
    public Task<IActionResult> BuyDelete([FromBody] UserDeleteSchema prop)
    {
        var buy = new BuyServices();
        
        var status = buy.Service.DeleteBuy(prop);
        
        
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
        var buy = new BuyServices();

        var status = buy.Service.GetPurchases(prop);

        if (status is null) return Task.FromResult<IActionResult>(NotFound("error, unable to execute the get command"));

        return Task.FromResult<IActionResult>(Ok(status));
    }
}