using System.Text.Json.Serialization;

namespace APIAnimeHub.Dto.Jikan
{
    public class AnimeSearchResponseDto
    {
        [JsonPropertyName("data")]
        public List<AnimeDto> Data { get; set; }
    }
}
