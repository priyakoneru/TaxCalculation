using Microsoft.Extensions.Configuration;
using Xunit;
using TaxCalculation.Models;
using Moq;
using System.Threading.Tasks;
using System.Net.Http;
using Moq.Protected;
using System.Net;
using TaxCalculation.Services;
using System.Collections.Generic;

namespace TaxCalculationTest.UnitTest
{
    public class UnitTestTaxRate
    {
      
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
        private readonly Mock<HttpClient> _mockHttpClient;
        private readonly IConfiguration _configuration;
        public UnitTestTaxRate()
        {

            _configuration = CreateConfiguration();
        }
        [Fact]
        public void GetRatesForLocation()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<System.Net.Http.HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<System.Net.Http.HttpResponseMessage>>("SendAsync", Moq.Protected.ItExpr.IsAny<System.Net.Http.HttpRequestMessage>(), Moq.Protected.ItExpr.IsAny<System.Threading.CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'rate': {'city': 'SANTA MONICA','city_rate': '0.0'}}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var result = new TaxService(_configuration, mockFactory.Object);
            var taxRate = result.RatesForLocation("90404");
            var expectedResult = GetTestRateForLocation();
            Assert.Equal(expectedResult.Rate.city, taxRate.Result.Rate.city);
        }
        [Fact]
        public void GetTotalOrderAmount()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<System.Net.Http.HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<System.Net.Http.HttpResponseMessage>>("SendAsync", Moq.Protected.ItExpr.IsAny<System.Net.Http.HttpRequestMessage>(), Moq.Protected.ItExpr.IsAny<System.Threading.CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'tax': {'amounttocollect': 1.09}}"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var result = new TaxService(_configuration, mockFactory.Object);
            var taxForOrder = result.TaxForOrder(GetTaxOrderRequest());
            var expectedResult = GetTestTotalOrderAmount();
            Assert.Equal(expectedResult.amount_to_collect, taxForOrder.Result.amount_to_collect);
        }
        private TaxOrderRequest GetTaxOrderRequest()
        {
            TaxOrderRequest taxOrderRequest = new TaxOrderRequest();
            taxOrderRequest.from_country = "US";
            taxOrderRequest.from_zip = "07001";
            taxOrderRequest.from_state = "NJ";
            return taxOrderRequest;
        }
        private TotalOrderAmount GetTestTotalOrderAmount()
        {

            TotalOrderAmount totalOrderAmount = new TotalOrderAmount();
            
            totalOrderAmount.amount_to_collect = 1.09M;
            return totalOrderAmount;
        }
        private TaxRate GetTestRateForLocation()
        {
            TaxRate taxRate = new TaxRate();
            taxRate.Rate.city = "SANTA MONICA";
            taxRate.Rate.city_rate = 0.0M;
            return taxRate;
        }

    private static IConfiguration CreateConfiguration()
    {
        var config = new Dictionary<string, string>
            {
                {"apikey", "value"}
            };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(config)
            .Build();
    }
}
}
