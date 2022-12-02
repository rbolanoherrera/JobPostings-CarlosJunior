using API.Entities;
using API.Models.DTO;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoryController : Controller
    {
        private readonly IDataRepository<JobCategory, JobCategoryDto> _dataRepository;

        public JobCategoryController(IDataRepository<JobCategory, JobCategoryDto> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var jobs = _dataRepository.GetAll();
            return Ok(jobs);
        }

        [HttpGet("{id}", Name = "GetJobCategory")]
        public IActionResult Get(int id)
        {
            var category = _dataRepository.GetDto(id);
            if (category == null)
            {
                return NotFound("JobCategory not found.");
            }

            return Ok(category);
        }
    }
}
