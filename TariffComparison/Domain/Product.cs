using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TariffComparison.Domain
{
    public class Product
    {
        public string Name { get; }

        public TariffType TariffType { get; }

        public decimal Price { get; }

        public int Consumption { get; }

        public Product
            (string name
            , TariffType tariffType
            , int consumption
            , Func<int, decimal> formula) 
        {
            Name = name;
            TariffType = tariffType;
            Consumption = consumption;
            Price = formula(consumption);
        }
    }

    public enum TariffType
    {
        Basic = 0,
        Packaged = 1
    }
}
