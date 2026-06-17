using APIAnimeHub.Dto;
using APIAnimeHub.Models;
using APIAnimeHub.Repositories;
using APIAnimeHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIAnimeHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
           private readonly UserRepository _userRepository;

           private readonly TokenService _tokenService;

        public AuthController(UserRepository userRepository, TokenService tokenService)
            {
                _userRepository = userRepository;
                _tokenService = tokenService;
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

                var token = _tokenService.GenerateToken(user);

                return Ok(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        // uma rota de teste que busca e trás o email a partir das claims
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new
            {
                Email = email
            });
        }
    }
}
