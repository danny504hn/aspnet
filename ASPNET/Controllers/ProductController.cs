using ASPNET.Interfaces;
using ASPNET.Model;
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
            
            return Ok(productes);
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
            if (price < 0) { return NotFound(price); }
            else return Ok(price);
        }

        [HttpGet("{productCode}/comandes")]

        public IActionResult GetOrders(string productCode)
        {
            List<int> orders = repository.GetOrders(productCode);
            return Ok(orders); ;

        }

        [HttpPost]
        public IActionResult Add(Product product) {
            repository.Add(product);
            return Created();
        }

        [HttpPut("{productCode}/stock")]
        public IActionResult UpdateStock(string productCode, int increment)
        {
            try
            {
                repository.UpdateStock(productCode, increment);
                return Ok(repository.GetOne(productCode));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{productCode}/price")]
        public IActionResult UpdatePrice(string productCode, decimal newPrice)
        {
            try
            {
                repository.UpdatePrice(productCode, newPrice);
                return Ok();
            }
            catch (ArgumentException ex)
            {
               
                return BadRequest(ex.Message);
            }

        }
    }
}
