using System.Text.Json.Serialization;

namespace APIAnimeHub.Dto.Jikan
{
    public class AnimeDto
    {
        [JsonPropertyName("mal_id")]
        public int MalId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("score")]
        public double? Score { get; set; }

        [JsonPropertyName("synopsis")]
        public string Synopsis { get; set; }

        [JsonPropertyName("images")]
        public ImagesDto Images { get; set; }
    }
}
