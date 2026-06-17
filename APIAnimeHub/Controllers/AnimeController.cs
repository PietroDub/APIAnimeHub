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
    }
}
