using API.Entities;
using API.Models.DTO;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IDataRepository<Publisher, PublisherDto> _dataRepository;

        public PublisherController(IDataRepository<Publisher, PublisherDto> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/publisher
        [HttpGet]
        public IActionResult Get()
        {
            var companies = _dataRepository.GetAll();
            return Ok(companies);
        }

        // GET: api/publisher/5
        [HttpGet("{id}", Name = "GetPublisher")]
        public IActionResult Get(int id)
        {
            var publisher = _dataRepository.GetDto(id);
            if (publisher == null)
            {
                return NotFound("Publisher not found.");
            }

            return Ok(publisher);
        }

        // POST: api/publisher
        [HttpPost]
        public IActionResult Post([FromBody] Publisher publisher)
        {
            if (publisher is null)
            {
                return BadRequest("Publisher is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Add(publisher);
            return CreatedAtRoute("GetPublisher", new { Id = publisher.Id }, null);
        }

        // PUT: api/Publisher/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Publisher publisher)
        {
            if (publisher == null)
            {
                return BadRequest("Publisher is null.");
            }

            var pubToUpdate = _dataRepository.Get(id);
            if (pubToUpdate == null)
            {
                return NotFound("The publisher record couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Update(pubToUpdate, publisher);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Publisher Id is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var objPublisher = _dataRepository.Get(id);
            if (objPublisher == null)
            {
                return NotFound("The Publisher record couldn't be found.");
            }

            _dataRepository.Delete(objPublisher);
            return NoContent();
        }
    }
}
