using APIAnimeHub.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAnimeHub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAnimeList> AnimeList { get; set; }
    }
}
