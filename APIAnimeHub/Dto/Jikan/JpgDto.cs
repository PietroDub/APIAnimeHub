using System.Text.Json.Serialization;

namespace APIAnimeHub.Dto.Jikan
{
    public class JpgDto
    {
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }
    }
}
