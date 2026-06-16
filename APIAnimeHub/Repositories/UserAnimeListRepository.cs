using APIAnimeHub.Data;
using APIAnimeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAnimeHub.Repositories
{
    public class UserAnimeListRepository
    {
        private readonly AppDbContext _context;

        public UserAnimeListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAnimeAsync(UserAnimeList anime) {
            _context.AnimeList.Add(anime);
            await _context.SaveChangesAsync();
        }

        public async Task<UserAnimeList?> AnimeAlreadyExists(Guid UserId, int AnimeId)
        {
            var animeList = await _context.AnimeList
                .Where(ul => ul.UserId == UserId)
                .ToListAsync();

            var anime = await _context.AnimeList.FirstOrDefaultAsync(a => a.AnimeId == AnimeId);

            if (anime != null) {
                return anime;
            }

            return null;
        }

        public async Task<List<UserAnimeList>> GetUserAnimeListsAsync(Guid userId)
        {
            return await _context.AnimeList
                .Where(ul => ul.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserAnimeList> GetByIdAsync(Guid id) =>
          await  _context.AnimeList.FirstOrDefaultAsync(Ul => Ul.Id == id); 

        public async Task UpdateAsync(UserAnimeList anime) { 
            _context.AnimeList.Update(anime);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserAnimeList anime)
        {
            _context.AnimeList.Remove(anime);
            await _context.SaveChangesAsync();
        }

    }
}
