using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculation.Models
{
    
    public class TotalOrderAmount
    {
        public double amount_to_collect { get; set; }
        public bool freight_taxable { get; set; }
        public bool has_nexus { get; set; }
        public TaxJurisdictions jurisdictions { get; set; }
        public double order_total_amount { get; set; }
        public double rate { get; set; }
        public double shipping { get; set; }
        public string tax_source { get; set; }
        public double taxable_amount { get; set; }
    }

    

}
