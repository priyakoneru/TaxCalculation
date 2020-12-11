using Newtonsoft.Json;

namespace TaxCalculation.Models
{
    public class TaxRate
    {
        [JsonProperty("rate")]
        public RateResponseAttributes Rate { get; set; }
    }

    public class RateResponseAttributes
    {
        public string city { get; set; }
        public decimal city_rate { get; set; }
        public decimal combined_district_rate { get; set; }

        public decimal combined_rate { get; set; }
        public string county { get; set; }

        public decimal county_rate { get; set; }

        public bool freight_taxable { get; set; }
        public string state { get; set; }

        public decimal state_rate { get; set; }
        public string zip { get; set; }
    }
    public class Rate
    {
        public string country { get; set; }

        public string zip { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public string street { get; set; }
    }
}
