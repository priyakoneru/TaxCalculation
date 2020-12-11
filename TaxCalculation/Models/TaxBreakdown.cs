using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculation.Models
{
    public class TaxBreakdown
    {
        public decimal state_tax_rate { get; set; }
        public decimal state_tax_collectable { get; set; }
        public decimal city_tax_collectable { get; set; }
        public decimal county_tax_collectable { get; set; }
        public decimal special_district_taxable_amount { get; set; }
        public decimal special_tax_rate { get; set; }
        public decimal special_district_tax_collectable { get; set; }
        public TaxBreakdownShipping shipping { get; set; }
        public List<TaxBreakdownLineItem> line_items { get; set; }
    }
}
