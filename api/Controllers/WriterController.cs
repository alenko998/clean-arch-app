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
        public async Task<ActionResult<IEnumerable<WriterDto>>> GetAll()
        {
            var writers = await _writerRepository.GetAllAsync();
            var result = writers.Select(WriterMapping.ToDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var writer = await _writerRepository.GetByIdAsync(id);
            if (writer == null) return NotFound($"Writer with ID {id} not found.");

            return Ok(WriterMapping.ToDto(writer));
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
                Password = $"{Convert.ToBase64String(salt)}.{hashed}",
                Role = "User"
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] WriterLoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Username and password are required.");

            var writer = await _writerRepository.GetByUsernameAsync(dto.Username);
            if (writer == null)
                return NotFound("Account with this username does not exist.");

            var parts = writer.Password.Split('.');
            if (parts.Length != 2)
                return Unauthorized("Stored password is invalid.");

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            var inputHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: dto.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            ));

            if (storedHash != inputHash)
                return Unauthorized("Invalid password.");

            var token = _tokenService.CreateToken(writer);

            return Ok(new WriterResponseDto
            {
                Id = writer.Id,
                Username = writer.Username,
                Token = token
            });
        }
    }
}
