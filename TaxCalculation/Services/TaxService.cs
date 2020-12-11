
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TaxCalculation.Models;
using System;
using System.Net.Mime;

namespace TaxCalculation.Services
{
    public class TaxService: ITaxService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public TaxService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
       
        public async Task<TaxRate> RatesForLocation(string zipCode)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _configuration.GetValue<string>("TaxJarApi"));
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.taxjar.com/v2/rates/" + zipCode);
                var response = await client.SendAsync(request);
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseStream = response.Content.ReadAsStringAsync().Result;
                        TaxRate taxRate = JsonConvert.DeserializeObject<TaxRate>(responseStream);
                        return taxRate;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }
        public async Task<TotalOrderAmount> TaxForOrder(TaxOrderRequest orderRequest)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.BaseAddress = new Uri("https://api.taxjar.com");
                string jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(orderRequest);
                HttpRequestMessage requestMessage = new HttpRequestMessage
                {
                    Content = new StringContent(jsonstring, System.Text.Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("v2/taxes", UriKind.Relative)
                };
                requestMessage.Headers.Add("Authorization", string.Format("Bearer {0}", _configuration.GetValue<string>("TaxJarApi")));
                try
                {
                    var result = await client.SendAsync(requestMessage);
                    if (result.IsSuccessStatusCode)
                    {
                        var responseStream = result.Content.ReadAsStringAsync().Result;
                        TotalOrderAmount orderAmount = JsonConvert.DeserializeObject<TotalOrderAmount>(responseStream);
                        return orderAmount;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }
    }
}
