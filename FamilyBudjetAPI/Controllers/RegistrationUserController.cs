using Domain.DAL.Entity;
using FamilyBudjetAPI;
using ManagingFinanceAPI.DTOModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagingFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationUserController : ControllerBase
    {
        private readonly FinanceContext _financeContext;

        public RegistrationUserController(FinanceContext context)
        {
            _financeContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            if (await _financeContext.UserRegistration.AnyAsync(u => u.Email == userDto.Email))
            {
                return BadRequest("Пользователь с такой почтой уже зарегистрирован.");
            }

            string passwordHash = HashPassword(userDto.Password);

            var user = new User
            {
                Email = userDto.Email,
                PasswordHash = passwordHash
            };

            _financeContext.UserRegistration.Add(user);
            await _financeContext.SaveChangesAsync();

            return Ok("Пользователь успешно зарегистрирован.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var user = await _financeContext.UserRegistration.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);

            if (user == null || !VerifyPassword(userLoginDto.Password, user.PasswordHash))
            {
                return BadRequest("Неправильная почта или пароль.");
            }

            var token = GenerateJwtToken(user, "123123123_іваіва23423423847а8мг3489347а8г3948е7348к7рівапорапіоапіо34757465765вапвапвамвапвапкапвапвап", 60);

            // Возврат JWT вместе с другими данными, если необходимо
            return Ok(new { Message = "Вход выполнен успешно", token });
        }

        private string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string GenerateJwtToken(User user, string secretKey, int expirationMinutes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        }),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}