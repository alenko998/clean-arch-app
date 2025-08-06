using api.DTOs.UserInfo;
using api.Mapping;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoRepository _repository;
        private readonly AppDbContext _context;

        public UserInfoController(IUserInfoRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var item = await _repository.GetByIdAsync(id);
            if (item is null) return NotFound($"UserInfo with ID {id} not found.");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserInfoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Email is required.");

            var entity = UserInfoMapping.ToEntity(dto);
            var created = await _repository.CreateAsync(entity);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserInfoDto dto)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var updated = await _repository.UpdateAsync(id, UserInfoMapping.ToEntityFromUpdate(dto));
            if (updated is null) return NotFound($"UserInfo with ID {id} not found.");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid ID.");

            var result = await _repository.DeleteAsync(id);
            if (result is null) return NotFound($"UserInfo with ID {id} not found.");

            return Ok(result);
        }

        [HttpPost("assign-to-writer/{writerId}")]
        public async Task<IActionResult> AssignToWriter([FromRoute] int writerId, [FromBody] AssignUserInfoDto dto)
        {
            var writer = await _context.Writers.FindAsync(writerId);
            if (writer == null)
                return NotFound($"Writer with ID {writerId} not found.");

            var info = new UserInfo
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            _context.UserInfos.Add(info);
            await _context.SaveChangesAsync();

            writer.UserInfoId = info.Id;
            await _context.SaveChangesAsync();

            return Ok("UserInfo successfully assigned to Writer.");
        }
    }
}
