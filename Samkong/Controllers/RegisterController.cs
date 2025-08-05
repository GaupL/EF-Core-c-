using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Samkong.AppContext;
using Samkong.Model;
using Samkong.Model.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Samkong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<Register> _manager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        public RegisterController(UserManager<Register> manager, IConfiguration config, ApplicationDbContext context)
        {
            _manager = manager;
            _config=config;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Register>>> get()
        {
          var user = await _context.Registers.ToListAsync();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterDTO>> getById(string id)
        {
            var user = await _context.Registers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new RegisterDTO()
            {
                NickName= user.NickName,
                Name=user.UserName,
                Email=user.Email,
            };
            return Ok(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterDTO model)
        {
            Register models = new Register()
            {
                FulllName= model.Name,
                NickName = model.NickName,
                Email = model.Email,
                UserName = model.Email,
                Address = model.Address,
                DOB = DateOnly.FromDateTime(DateTime.Now.AddYears(-model.Age))
            };
            var result = await _manager.CreateAsync(models,model.Password!);
            await _manager.AddToRoleAsync(models, model.Role!);
            if(result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login model)
        {
            var user = await _manager.FindByEmailAsync(model.Email!);
            var pass = await _manager.CheckPasswordAsync(user, model.Password);
            if (user != null && pass)
            {
                var roles = await _manager.GetRolesAsync(user);
                var signingKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["AppSettings:JWTSecret"]!));
                ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim("User",user.Email.ToString()),
                    new Claim("Name",user.NickName.ToString()),
                    new Claim("UserId",user.Id.ToString()),
                    new Claim(ClaimTypes.Role,roles.First())
                });
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(
                        signingKey,
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            return BadRequest();
        }
    }
}
