using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculation.Models
{
    public class TaxBreakdownLineItem
    {
        public string id { get; set; }
        public decimal state_sales_tax_rate { get; set; }
        public decimal state_amount { get; set; }
        public decimal county_amount { get; set; }
        public decimal city_amount { get; set; }
        public decimal special_district_taxable_amount { get; set; }
        public decimal special_tax_rate { get; set; }
        public decimal special_district_amount { get; set; }
    }
}
