using System;
using TariffComparison.Domain;
using Xunit;

namespace TariffComparison.Tests
{
    public class TariffCalculatorTests
    {
        [Theory]
        [InlineData(5, 0.22, 100, 82)]
        [InlineData(5, 0.22, 1500, 390)]
        [InlineData(5, 0.22, 3500, 830)]
        [InlineData(5, 0.22, 4500, 1050)]
        [InlineData(5, 0.22, 6000, 1380)]
        [InlineData(8, 1, 4500, 4596)]
        [InlineData(9.5, 1.5, 256, 498)]
        public void BasicTariffTests(decimal monthlyBasePrice, decimal kWhPrice, int consumption, decimal result)
        {
            Assert.Equal(TariffCalculator.Basic(monthlyBasePrice, kWhPrice)(consumption), result);
        }

        [Theory]
        [InlineData(800, 0.3, 4000, 0, 800)]
        [InlineData(800, 0.3, 4000, 100, 800)]
        [InlineData(800, 0.3, 4000, 3500, 800)]
        [InlineData(800, 0.3, 4000, 4000, 800)]
        [InlineData(800, 0.3, 4000, 4500, 950)]
        [InlineData(800, 0.3, 4000, 6000, 1400)]
        [InlineData(600, 0.6, 3000, 3500, 900)]
        [InlineData(1000, 0.8, 2500, 6000, 3800)]
        public void PackagedTariffTests(decimal baseLinePrice, decimal kWhPrice, int baseLineConsumption, int consumption, decimal result)
        {
            Assert.Equal(TariffCalculator.Packaged(baseLinePrice, kWhPrice, baseLineConsumption)(consumption), result);
        }
    }
}
