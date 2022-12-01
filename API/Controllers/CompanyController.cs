using API.Entities;
using API.Models.DTO;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IDataRepository<Company, CompanyDto> _dataRepository;

        public CompanyController(IDataRepository<Company, CompanyDto> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Company
        [HttpGet]
        public IActionResult Get()
        {
            var companies = _dataRepository.GetAll();
            return Ok(companies);
        }

        // GET: api/Company/5
        [HttpGet("{id}", Name = "GetCompany")]
        public IActionResult Get(int id)
        {
            var company = _dataRepository.GetDto(id);
            if (company == null)
            {
                return NotFound("Company not found.");
            }

            return Ok(company);
        }

        // POST: api/Companies
        [HttpPost]
        public IActionResult Post([FromBody] Company company)
        {
            if (company is null)
            {
                return BadRequest("Company is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(company);
            return CreatedAtRoute("GetCompany", new { Id = company.Id }, null);
        }

        // PUT: api/Companies/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Company company)
        {
            if (company == null)
            {
                return BadRequest("Company is null.");
            }

            var companyToUpdate = _dataRepository.Get(id);
            if (companyToUpdate == null)
            {
                return NotFound("The Company record couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Update(companyToUpdate, company);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Company Id is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var objPub = _dataRepository.Get(id);
            if (objPub == null)
            {
                return NotFound("The Publisher record couldn't be found.");
            }

            _dataRepository.Delete(objPub);
            return NoContent();
        }

    }
}
