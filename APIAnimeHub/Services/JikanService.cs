using APIAnimeHub.Dto.Jikan;
using System.Text.Json;

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

        public async Task<AnimeSearchResponseDto> GetAnimeByName(string title)
        {
            try
            {
                // tira espaços e formata a digitação
                var query = Uri.EscapeDataString(title);

                var response = await _httpClient.GetAsync(
                    $"https://api.jikan.moe/v4/anime?q={query}"
                );

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                // desserializa o json
                var result = JsonSerializer.Deserialize<AnimeSearchResponseDto>(json);

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(
                    $"Erro ao consultar Jikan: {ex.Message}"
                );
            }
        }

        public async Task<AnimeDetailsSearchResponseDto> GetAnimeById(int id)
        {
            var response = await _httpClient.GetAsync(
                $"https://api.jikan.moe/v4/anime/{id}"
            );

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AnimeDetailsSearchResponseDto>(json);

                return result;
            }
            else
            {
                throw new Exception($"Erro ao consultar Jikan com o id: {id}");
            }
        }

        public async Task<AnimeSearchResponseDto?> GetAnimeTopAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/top/anime");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                // desserializa o json
                var result = JsonSerializer.Deserialize<AnimeSearchResponseDto>(json);

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(
                    $"Erro ao consultar Jikan: {ex.Message}"
                );
            }
        }

        public async Task<AnimeSearchResponseDto?> GetCurrentSeason()
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/seasons/now");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                // Desserializa o json
                var result = JsonSerializer.Deserialize<AnimeSearchResponseDto>(json);

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(
                    $"Erro ao consultar Jikan: {ex.Message}"
                );
            }
        }

        public async Task<SeasonsArchiveResponseDto?> GetAllSeasons()
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/seasons");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<SeasonsArchiveResponseDto>(
                    json,
                    new JsonSerializerOptions {
                        PropertyNameCaseInsensitive = true
                    });

                return result;
            }
            catch (HttpRequestException ex) {
                
                throw new Exception(
                $"Erro ao consultar Jikan: {ex.Message}"
                
                );
            }
        }

        public async Task<AnimeSearchResponseDto> GetOneSeason(int year, string station)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/seasons/{year}/{station}");

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<AnimeSearchResponseDto>(
                    json,
                    new JsonSerializerOptions
                    {
                        //Ele ignora diferença de:
                        //maiúsculas vs minúsculas
                        //PascalCase vs camelCase
                        PropertyNameCaseInsensitive = true
                    }
                );

                return result;
            }
            catch(HttpRequestException ex)
            {
                throw new Exception(
                    $"Erro ao consultar Jikan: {ex.Message}"
                );
            }
        }
    }
}
