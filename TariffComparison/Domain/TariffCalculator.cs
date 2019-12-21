using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TariffComparison.Domain
{
    public static class TariffCalculator
    {
        public static Func<int, decimal> Basic(
            decimal monthlyBasePrice
            , decimal kWhPrice) =>
            consumption =>
            {
                return 12 * monthlyBasePrice + consumption * kWhPrice;
            };

        public static Func<int, decimal> Packaged(
            decimal baseLinePrice
            , decimal kWhPrice
            , int baseLineConsumption)
        {
            return consumption =>
            {
                if (consumption <= baseLineConsumption)
                    return baseLinePrice;
                
                return baseLinePrice + (consumption - baseLineConsumption) * kWhPrice;
            };
        }
    }
}
