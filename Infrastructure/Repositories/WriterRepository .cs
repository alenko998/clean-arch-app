using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WriterRepository : IWriterRepository
    {
        private readonly AppDbContext _context;

        public WriterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Writer>> GetAllAsync()
        {
            return await _context.Writers.ToListAsync();
        }
    }
}
