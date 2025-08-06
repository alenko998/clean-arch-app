using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using api.Mapping;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WriterController : ControllerBase
    {
        private readonly IWriterRepository _writerRepository;

        public WriterController(IWriterRepository writerRepository)
        {
            _writerRepository = writerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Writer>>> GetAll()
        {
            var writers = await _writerRepository.GetAllAsync();
            var result = writers.Select(WriterMapping.ToDto);
            return Ok(result);
        }
    }
}
