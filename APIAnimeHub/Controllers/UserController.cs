using APIAnimeHub.Dto;
using APIAnimeHub.Models;
using APIAnimeHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]

        public async Task<ActionResult<User?>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }

        [HttpPut("{id}")]

        //public async Task<ActionResult> UpdateUserAsync(User user)
        //{
        //    await _userRepository.UpdateAsync(user);

        //    return Ok();
        //}

        public async Task<ActionResult> UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
           var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return NotFound("Usuário não encontrado.");

            user.Name = dto.Name;
            user.Email = dto.Email;

            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync (Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return NotFound("Usuário não encontrado.");

            await _userRepository.DeleteAsync(user);

            return NoContent();
        }

    }
}
