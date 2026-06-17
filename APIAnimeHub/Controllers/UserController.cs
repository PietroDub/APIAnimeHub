using APIAnimeHub.Dto;
using APIAnimeHub.Models;
using APIAnimeHub.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace APIAnimeHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUserById(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim);

            if (userId != id)
            {
                // status HTTP que indica que o acesso à página ou recurso solicitado é proibido por algum motivo
                return Forbid();
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]

        //public async Task<ActionResult> UpdateUserAsync(User user)
        //{
        //    await _userRepository.UpdateAsync(user);

        //    return Ok();
        //}

        public async Task<ActionResult> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim);

            if (userId != id) {
                return Forbid();
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return NotFound("Usuário não encontrado.");

            user.Name = dto.Name;
            user.Email = dto.Email;

            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync (Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim);

            if (userId != id) {
                return Forbid();
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return NotFound("Usuário não encontrado.");

            await _userRepository.DeleteAsync(user);

            return NoContent();
        }

    }
}
