using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaxCalculation.Models;

namespace TaxCalculation.Controllers
{
    public class TaxRateController : Controller
    {
        private readonly ITaxService _taxService;
        public TaxRateController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [Route("[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> RatesForLocation(string zipCode)
        {
            try
            {
                var rate = await _taxService.RatesForLocation(zipCode);
                return Ok(rate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<TotalOrderAmount>> TaxForOrder([FromBody] TaxOrderRequest orderRequest)
        {
            try
            {
                var rate = await _taxService.TaxForOrder(orderRequest);
                return Ok(rate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
