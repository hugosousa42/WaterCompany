using WaterCompany.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WaterCompany.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillsController : Controller
    {
        private readonly IBillRepository _billRepository;

        public BillsController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        [HttpGet("last-bill/{userId}")]
        public IActionResult GetBill(string userId)
        {
            return Ok(_billRepository.GetLastBillToPay(userId));
        }
    }
}
