using ASPNET.Interfaces;
using ASPNET.Model;
using Microsoft.AspNetCore.Mvc;
using ASPNET.Interfaces;
using ASPNET.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(string productLineId)
        {

            var productes = repository.GetAll(productLineId);
            if (!productes.Any()) return NotFound(new List<Product>()); //Potser la list buida es aixi o hi ha alguna otra manera pero transformar productes en empty
            else return Ok(productes);
        }

        [HttpGet("{productCode}")] //Perque al swagger demana product code e id?
        public IActionResult GetOne(string productCode) {
            Product p = repository.GetOne(productCode);
            if (p == null) return NotFound(p);
            return Ok(p);
        }

        [HttpGet("{productCode}/preu")]

        public IActionResult GetPrice(string productCode) { 
            decimal price = repository.GetPrice(productCode);
            if(price < 0) { return NotFound(price); }
            else return Ok(price);
        }

        [HttpGet("{productCode}/comandes")]

        public IActionResult GetOrders(string productCode)
        {
            List<int> orders = repository.GetOrders(productCode);
            if (!orders.Any()) { return NotFound(new List<int>()); }
            else return Ok(orders);

        }

        [HttpPost]
        public IActionResult Add(Product product) {
            repository.Add(product);
            return Created();
        }

        [HttpPut]
        public IActionResult UpdateStock(String productCode, int increment)
        {
            repository.UpdateStock(productCode, increment);
            return Ok(); 
        }

        [HttpPut]
        public IActionResult UpdatePrice(String productCode, int increment)
        {
            repository.UpdatePrice(productCode, increment);
            return Ok();
        }
    }
}
