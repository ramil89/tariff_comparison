using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TariffComparison.Domain;
using TariffComparison.Presets;

namespace TariffComparison.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BasicTariffPresets _basicPresets;

        private readonly PackagedTariffPresets _packagedPresets;

        public ProductRepository(
            BasicTariffPresets basicPresets
            , PackagedTariffPresets packagedPresets)
        {
            _basicPresets = basicPresets;
            _packagedPresets = packagedPresets;
        }

        public IEnumerable<Product> EvaluateTariff(int consumption)
        {
            yield return new Product("Basic tariff",
                TariffType.Basic,
                consumption,
                TariffCalculator.Basic(
                    _basicPresets.MonthlyBasePrice
                    , _basicPresets.KWHPrice));

            yield return new Product("Packaged tariff",
                TariffType.Packaged,
                consumption,
                TariffCalculator.Packaged(
                    _packagedPresets.BaseLinePrice
                    , _packagedPresets.KWHPrice
                    , _packagedPresets.BaseLineConsumption));

        }
    }
}
