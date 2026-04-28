using ASPNET.Interfaces;
using ASPNET.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductLineController : ControllerBase
    {
        private readonly IProductLineRepository repository;

        public ProductLineController(IProductLineRepository repository) {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var productLines = repository.GetAll();
            return Ok(productLines);

        }

        [HttpGet("{id}")]
        public IActionResult GetOne(string id) {
            var productline = repository.GetOne(id);
            if(productline == null) return NotFound();
            else return Ok(productline);
        }

        [HttpGet("substring")]
        public IActionResult GetFromDescription(string subTextDescription) {

            List<ProductLine> productlines = repository.GetFromDescription(subTextDescription);
            return Ok(productlines);


        }

        [HttpPost]
        public IActionResult Add(ProductLine productline) {
            repository.Add(productline);
            return Created();
        }

        [HttpPut]
        public IActionResult Update(ProductLine productline)
        {
            ProductLine plToUpdate = repository.GetOne(productline.ProductLineId);
            if (plToUpdate != null)
            {
                repository.Update(productline);
                return Ok(productline);

            }
            else return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            ProductLine pl = repository.GetOne(id);
            if (pl == null) return NotFound();

            repository.Delete(id);
            return Ok();

        }



    }
}
