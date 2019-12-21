using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TariffComparison.Presets
{
    public class PackagedTariffPresets
    {
        public decimal BaseLinePrice { get; set; }

        public int BaseLineConsumption { get; set; }

        public decimal KWHPrice { get; set; }
    }
}
