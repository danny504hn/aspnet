using ASPNET.Interfaces;

namespace ASPNET.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OfficeController
    {
        private readonly IOfficeRepository repository;

        public OfficeController(IOfficeRepository repository) {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            repository.GetAll();
        }

        [HttpGet("{officeCode}")]
        public IActionResult GetOne(string officeCode)
        {
            return repository.GetOne(officeCode);
        }

        [HttpPut]
        public IActionResult UpdatePostalCode(string officeCode, string newOfficeCode)
        {
            repository.UpdatePostalCode(officeCode, newOfficeCode);
        }
    }
}
