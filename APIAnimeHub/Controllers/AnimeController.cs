using APIAnimeHub.Dto.Jikan;
using APIAnimeHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAnimeHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly JikanService _jikanService;

        public AnimeController(JikanService jikanService)
        {
            _jikanService = jikanService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var result = await _jikanService.TestAsync();

            return Ok(result);
        }

        [HttpGet("titulo")]
        public async Task<AnimeSearchResponseDto> SearchAnimeAsync(string title)
        {
            var result = await _jikanService.GetAnimeByName(title);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<AnimeDetailsSearchResponseDto> SearchAnimeByIdAsync(int id) { 
            var result = await _jikanService.GetAnimeById(id);

            return result;
        }

        [HttpGet("Top")]
        public async Task<AnimeSearchResponseDto?> GetTopAnimes()
        {
            var result = await _jikanService.GetAnimeTopAsync();

            return result;
        }

        [HttpGet("season")]
        public async Task<ActionResult> GetCurrentSeason()
        {
            var animes = await _jikanService.GetCurrentSeason();

            return Ok(animes);
        }
    }
}
