namespace APIAnimeHub.Models
{
    public class UserAnimeList
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int AnimeId { get; set; }
        public AnimeStatus Status { get; set; }
        public int? EpisodesWatched { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? Score { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
