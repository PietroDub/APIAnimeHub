using APIAnimeHub.Dto;
using APIAnimeHub.Models;
using APIAnimeHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAnimeHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
           private readonly UserRepository _userRepository;
            public AuthController(UserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            [HttpPost]
            public async Task<ActionResult> RegisterUser(RegisterUserDto dto)
            {
            var exists = await _userRepository.GetByEmailAsync(dto.Email);

            if (exists != null)
            {
                return BadRequest("Email já cadastrado.");
            }

            var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password)
                };

                await _userRepository.AddAsync(user);

                return Ok(user);
            }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser (LoginDto dto)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(dto.Email);

                if(user == null)
                {
                    return Unauthorized("Email ou senha inválidos");
                }

                bool PasswordTrue = BCrypt.Net.BCrypt.EnhancedVerify(dto.Password, user.PasswordHash);

                if (!PasswordTrue)
                {
                    return Unauthorized("Email ou senha inválidos");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
