using API.Entities;
using API.Models.DTO;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IDataRepository<Job, JobDto> _dataRepository;

        public JobController(IDataRepository<Job, JobDto> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/job
        [HttpGet]
        public IActionResult Get()
        {
            var jobs = _dataRepository.GetAll();
            return Ok(jobs);
        }

        // GET: api/job/5
        [HttpGet("{id}", Name = "GetJob")]
        public IActionResult Get(int id)
        {
            var job = _dataRepository.Get(id);
            if (job == null)
            {
                return NotFound("Job not found.");
            }

            return Ok(job);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Job job)
        {
            if (job is null)
            {
                return BadRequest("job is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(job);
            return CreatedAtRoute("GetJob", new { Id = job.Id }, null);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Job Id is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var objJob = _dataRepository.Get(id);
            if (objJob == null)
            {
                return NotFound("The Job record couldn't be found.");
            }

            _dataRepository.Delete(objJob);
            return NoContent();
        }

    }
}
