using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TariffComparison.Domain;

namespace TariffComparison.Repository
{
    public interface IProductRepository
    {
        public IEnumerable<Product> EvaluateTariff(int consumption);
    }
}
