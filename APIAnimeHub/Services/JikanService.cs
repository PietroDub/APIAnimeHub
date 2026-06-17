namespace APIAnimeHub.Services
{
    public class JikanService
    {
        private readonly HttpClient _httpClient;

        public JikanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TestAsync()
        {
            var response = await _httpClient.GetAsync(
                "https://api.jikan.moe/v4/anime?q=naruto"
            );

            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}
