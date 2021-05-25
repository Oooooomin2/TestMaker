using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestMakerProject.Controllers.Resources;
using TestMakerProject.Models;
using TestMakerProject.Persistence;

namespace TestMakerProject.Controllers
{
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly TestMakerContext _context;
        private readonly IMapper _mapper;

        public UserController(TestMakerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.UserId == id);
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _mapper.Map(userResource, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<User, UserResource>(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}
