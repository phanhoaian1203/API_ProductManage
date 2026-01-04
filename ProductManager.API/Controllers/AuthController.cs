using Microsoft.AspNetCore.Mvc;
using ProductManager.API.DTOs;
using ProductManager.Core.Entities;
using ProductManager.Core.Interfaces;

namespace ProductManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Mật khẩu xác nhận không khớp.");
            }

            try
            {
                // Map từ DTO sang Entity
                var newUser = new User
                {
                    Username = request.Username,
                    Role = "Customer" // Mặc định role
                };

                await _authService.RegisterAsync(newUser, request.Password);

                return Ok(new { message = "Đăng ký thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var token = await _authService.LoginAsync(request.Username, request.Password);

            if (token == null)
            {
                return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu." });
            }

            return Ok(new { token = token });
        }
    }
}