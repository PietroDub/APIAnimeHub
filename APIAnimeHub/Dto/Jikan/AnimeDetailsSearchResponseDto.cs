using System.Text.Json.Serialization;

namespace APIAnimeHub.Dto.Jikan
{
    public class AnimeDetailsSearchResponseDto
    {
        [JsonPropertyName("data")]
        public AnimeDto Data { get; set; }
    }
}
