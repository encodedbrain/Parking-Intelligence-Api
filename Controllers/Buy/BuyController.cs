using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Controllers.Buy
{
    [ApiController]
    [Route("v1")]
    public class BuyController : ControllerBase
    {
        [HttpPost]
        [Route("user/buy")]
        [Authorize]
        public async Task<IActionResult> Buy([FromBody] BuySchema purchase)
        {
            BuyServices Buy = new BuyServices();

            var b = Buy.MakingPurchase(purchase);
            

            return Ok("oi");
        }
    }
}
