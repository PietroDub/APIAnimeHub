using APIAnimeHub.Data;
using APIAnimeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAnimeHub.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync(); //confirma a inserção
        }

        // User Controller

        //GET    /api/users/{id

        public async Task<User?> GetByIdAsync(Guid Id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);

        // PUT    /api/users/{id}

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        // DELETE /api/users/{id}

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

    }
}

