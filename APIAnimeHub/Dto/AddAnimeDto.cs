using APIAnimeHub.Models;

namespace APIAnimeHub.Dto
{
    public class AddAnimeDto
    {
        //   agora faremos com JWT
        //public Guid UserId { get; set; }

        public int AnimeId { get; set; }

        public AnimeStatus Status { get; set; }

        public int? Score { get; set; }
    }
}
