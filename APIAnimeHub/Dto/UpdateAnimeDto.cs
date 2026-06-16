using APIAnimeHub.Models;

namespace APIAnimeHub.Dto
{
    public class UpdateAnimeDto
    {
        public AnimeStatus Status { get; set; }
        public int? EpisodesWatched { get; set; }
        public int? Score { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
