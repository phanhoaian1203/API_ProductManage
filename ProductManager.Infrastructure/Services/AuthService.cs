using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductManager.Core.Entities;
using ProductManager.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManager.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            // 1. Lấy user từ Repo
            var user = await _userRepo.GetByUsernameAsync(username);

            // 2. Kiểm tra user tồn tại & password khớp
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null; // Đăng nhập thất bại
            }

            // 3. Tạo Token
            return CreateToken(user);
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            // 1. Check tồn tại qua Repo
            if (await _userRepo.ExistsAsync(user.Username))
            {
                throw new Exception("Username đã tồn tại.");
            }

            // 2. Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = passwordHash;

            // 3. Lưu user qua Repo
            await _userRepo.AddAsync(user);

            return user;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Lấy key từ appsettings.Development.json
            var secretKey = _configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}