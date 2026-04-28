using ASPNET.Interfaces;
using ASPNET.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeRepository repository;

        public OfficeController(IOfficeRepository repository) {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Office> offices = repository.GetAll();
            if (offices.Count > 0) { return Ok(offices); }
            else return NotFound(offices);
        
        }

        [HttpGet("{officeCode}")]
        public IActionResult GetOne(string officeCode)
        {
            Office o = repository.GetOne(officeCode);
            if (o != null) return Ok(o);
            else return NotFound();
        
        }

        [HttpPut]
        public IActionResult UpdatePostalCode(string officeCode, string newPostalCode)
        {
            repository.UpdatePostalCode(officeCode, newPostalCode);
            return Ok();
        }
        
            
    }
}
