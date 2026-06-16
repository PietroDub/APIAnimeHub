using APIAnimeHub.Dto;
using APIAnimeHub.Models;
using APIAnimeHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace APIAnimeHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnimeListController : ControllerBase
    {
        private readonly UserAnimeListRepository _userAnimeListRepository;
        private readonly UserRepository _userRepository;

        public UserAnimeListController(UserAnimeListRepository userAnimeListRepository, UserRepository userRepository)
        {

            _userAnimeListRepository = userAnimeListRepository;
            _userRepository = userRepository;
        }
        [HttpPost]
        public async Task<ActionResult> AdicionarAnimeAsync(AddAnimeDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);

            if (user == null)
            {
                return NotFound("User não encontrado");
            }
            var exists = await _userAnimeListRepository.AnimeAlreadyExists(dto.UserId, dto.AnimeId);

            if (exists != null)
            {
                return BadRequest(
                    "Anime já está na lista."
                );
            }

            var anime = new UserAnimeList
            {
                UserId = dto.UserId,
                AnimeId = dto.AnimeId,
                AddedDate = DateTime.UtcNow,
                Status = dto.Status
            };

            if (dto.Status == AnimeStatus.Completed)
            {
                anime.CompletedAt = DateTime.UtcNow;
            }

            await _userAnimeListRepository.AddAnimeAsync(anime);

            return Ok(anime);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetUserAnimeList(Guid userId)
        {
            var animeList = await _userAnimeListRepository.GetUserAnimeListsAsync(userId);
            if (!animeList.Any())
            {
                return NotFound("Nenhum anime encontrado.");
            }
            return Ok(animeList);
        }

        [HttpGet("Anime/{id}")]

        public async Task<ActionResult> GetAnimeByIdAsync(Guid id)
        {
            var animeListItem = await _userAnimeListRepository.GetByIdAsync(id);

            if (animeListItem == null)
            {
                return NotFound("animeListItem não encontrado");
            }

            return Ok(animeListItem);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateAnimeListAsync(UpdateAnimeDto dto, Guid id)
        {
            var anime = await _userAnimeListRepository.GetByIdAsync(id);

            if (anime == null)
            {
                return NotFound("Anime não existe!");
            }

            if (dto.Status == AnimeStatus.Completed && anime.CompletedAt == null)
            {
                anime.CompletedAt = DateTime.UtcNow;
            }
            if (dto.EpisodesWatched < 0)
            {
                return BadRequest("Quantidade inválida.");
            }


            anime.Status = dto.Status;
            anime.EpisodesWatched = dto.EpisodesWatched;
            
            await _userAnimeListRepository.UpdateAsync(anime);

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteAnimeAsync(Guid id)
        {
            var anime = await _userAnimeListRepository.GetByIdAsync(id);

            if (anime == null)
            {
                return NotFound("Anime não existe!");
            }

            await _userAnimeListRepository.DeleteAsync(anime);
            return NoContent();

        }
    }
}
