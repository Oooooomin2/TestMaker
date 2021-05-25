using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestMakerProject.Models;
using TestMakerProject.Persistence;

namespace TestMakerProject.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly TestMakerContext _context;
        private readonly ApplicationSettings _appsettings;

        public AccountController(TestMakerContext context, IOptions<ApplicationSettings> appsettings)
        {
            _context = context;
            _appsettings = appsettings.Value;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(o => o.LoginId == login.LoginId);
            if(user != null && user.Password == login.Password)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.UserId.ToString())
                    }),
                    Expires = DateTime.Now.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.JWT_Secret)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { user.UserName, token, user.UserId });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
