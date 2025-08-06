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
        public async Task<Writer?> GetByUsernameAsync(string username)
        {
            return await _context.Writers.FirstOrDefaultAsync(w => w.Username == username);
        }

        public async Task<Writer> CreateAsync(Writer writer)
        {
            _context.Writers.Add(writer);
            await _context.SaveChangesAsync();
            return writer;
        }
    }
}
