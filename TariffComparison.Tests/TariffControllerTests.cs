using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TariffComparison.Controllers;
using Xunit;
using System.Linq;

namespace TariffComparison.Tests
{
    public class TariffControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TariffControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(500)]
        [InlineData(2000)]
        [InlineData(3500)]
        [InlineData(5000)]
        public async Task TariffsPrice_In_AscendingOrderTestAsync(int consumption)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/tariff/compare/{consumption}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var tariffs = JsonConvert.DeserializeObject<List<ResponseTariffItem>>(content);

            Assert.True(tariffs.Count > 0);

            (decimal Price, bool IsAscending) seed = (0, true);

            var result = tariffs.Aggregate(seed, (state, tariff) =>
              {
                  if (!state.IsAscending)
                      return state;

                  var currentPrice = decimal.Parse(tariff.Price);
                  if (decimal.Compare(state.Price, currentPrice) > 0)
                  {
                      state.IsAscending = false;
                  }

                  state.Price = currentPrice;
                  return state;
              });

            Assert.True(result.IsAscending);
        }
    }
}
