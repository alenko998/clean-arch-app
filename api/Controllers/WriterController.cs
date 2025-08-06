using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using api.Mapping;
using api.DTOs.Writer;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WriterController : ControllerBase
    {
        private readonly IWriterRepository _writerRepository;
        private readonly ITokenService _tokenService;

        public WriterController(IWriterRepository writerRepository, ITokenService tokenService)
        {
            _writerRepository = writerRepository;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Writer>>> GetAll()
        {
            var writers = await _writerRepository.GetAllAsync();
            var result = writers.Select(WriterMapping.ToDto);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] WriterRegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || dto.Username.Length < 6)
                return BadRequest("Username is required and must be at least 6 characters long.");

            if (string.IsNullOrWhiteSpace(dto.Password) || !dto.Password.Any(char.IsDigit))
                return BadRequest("Password is required and must contain at least one number.");

            var existing = await _writerRepository.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return Conflict("Username is already taken.");

            // Hash password
            var salt = RandomNumberGenerator.GetBytes(16);
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: dto.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            ));

            var writer = new Writer
            {
                Username = dto.Username,
                Password = $"{Convert.ToBase64String(salt)}.{hashed}"
            };

            var created = await _writerRepository.CreateAsync(writer);
            var token = _tokenService.CreateToken(created);

            return Ok(new WriterResponseDto
            {
                Id = created.Id,
                Username = created.Username,
                Token = token
            });
        }
    }
}
