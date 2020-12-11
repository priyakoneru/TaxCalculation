using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculation.Models
{
    public interface ITaxService
    {
        Task<TaxRate> RatesForLocation(string zipCode);
        Task<TotalOrderAmount> TaxForOrder(TaxOrderRequest orderRequest);
    }
}
