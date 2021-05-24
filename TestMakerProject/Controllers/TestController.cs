using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestMakerProject.Controllers.Resources;
using TestMakerProject.Models;
using TestMakerProject.Persistence;

namespace TestMakerProject.Controllers
{
    [Route("/api/test")]
    public class TestController : Controller
    {
        private readonly TestMakerContext _context;
        private readonly IMapper _mapper;

        public TestController(TestMakerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<TestResource> GetTest(int id)
        {
            var tests = await _context.Tests
                .Include(m => m.Questions)
                    .ThenInclude(m => m.Choices)
                .SingleOrDefaultAsync(m => m.TestId == id);
            return _mapper.Map<Test, TestResource>(tests);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] TestResource testResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var test = _mapper.Map<TestResource, Test>(testResource);
            test.CreatedTime = DateTime.Now;
            test.UpdatedTime = DateTime.Now;
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<Test, TestResource>(test);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(int id, [FromBody] TestResource testResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var test = await _context.Tests.FindAsync(id);

            if (test == null)
                return NotFound();

            _mapper.Map(testResource, test);
            test.UpdatedTime = DateTime.Now;
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<Test, TestResource>(test);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            if (test == null)
                return NotFound();

            _context.Remove(test);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}
