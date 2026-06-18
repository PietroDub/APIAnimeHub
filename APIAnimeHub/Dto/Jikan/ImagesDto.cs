using System.Text.Json.Serialization;

namespace APIAnimeHub.Dto.Jikan
{
    public class ImagesDto
    {
        [JsonPropertyName("jpg")]
        public JpgDto Jpg { get; set; }
    }
}
