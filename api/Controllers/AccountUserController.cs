using api.DTOs.AccountUser;
using api.Mapping;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountUserController : ControllerBase
    {
        private readonly IAccountUserRepository _repository;
        private readonly AppDbContext _context;
        public AccountUserController(IAccountUserRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountUser>>> GetAll()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.GetByIdAsync(id); // repository mora da koristi .Include!
            if (user == null) return NotFound();
            return Ok(AccountUserMapping.ToDto(user));
        }




        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = AccountUserMapping.ToEntity(dto);
            var created = await _repository.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _repository.UpdateAsync(id, AccountUserMapping.ToEntityFromUpdate(dto));
            if (updated is null) return NotFound($"User with ID {id} not found.");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.AccountUsers.FindAsync(id);

            if (user == null)
                return NotFound($"User with ID {id} not found.");

            // Ako postoji povezan UserInfo → obriši ga
            if (user.UserInfoId.HasValue)
            {
                var userInfo = await _context.UserInfos.FindAsync(user.UserInfoId.Value);
                if (userInfo != null)
                {
                    _context.UserInfos.Remove(userInfo);
                }
            }

            _context.AccountUsers.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"User with ID {id} and associated UserInfo deleted (if existed).");
        }
    }
}
