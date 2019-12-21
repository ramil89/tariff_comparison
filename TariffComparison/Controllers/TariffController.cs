using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Repository;
using System.ComponentModel.DataAnnotations;

namespace TariffComparison.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public TariffController(IProductRepository productRepository)
            => _productRepository = productRepository;

        public string Get()
        {
            return "ok";
        }

        [HttpGet("compare/{consumption}")]
        public IEnumerable<ResponseTariffItem> Compare([Range(0, int.MaxValue)]int consumption)
        {
            return _productRepository.EvaluateTariff(consumption)
                .OrderBy(o => o.Price)
                .Select(s => new ResponseTariffItem(s.Name, s.Price.ToString("F2")))
                .ToList();
        }
    }

    public class ResponseTariffItem
    {
        public ResponseTariffItem(string name, string price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }

        public string Price { get; }
    }
}