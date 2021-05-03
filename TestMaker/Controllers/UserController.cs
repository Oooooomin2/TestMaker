using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMaker.Data;
using TestMaker.Models;

namespace TestMaker.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly TestMakerContext _context;

        public UserController(TestMakerContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [AllowAnonymous]
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,LoginId,UserName,Password")] User user)
        {
            if(_context.Users.Where(o => o.LoginId == user.LoginId).Any())
            {
                return View(user);
            }
            if (ModelState.IsValid)
            {
                var saltBytes = Password.CreateSalt(Password.saltSize);
                var hashBytes = Password.CreatePBKDF2Hash(user.Password, saltBytes, Password.hashSize, Password.iteration);
                user.Salt = Password.ChangeToBase64(saltBytes);
                user.Password = Password.ChangeToBase64(hashBytes);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,LoginId,Password,Salt,UserName,SelfIntroduction,Icon")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            var files = Request.Form.Files;
            if (files.Any())
            {
                using var memoryStream = new MemoryStream();
                await files.FirstOrDefault().CopyToAsync(memoryStream);

                user.Icon = memoryStream.ToArray();
            }

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                if (User.FindFirst(ClaimTypes.Name).Value != user.UserName)
                {
                    Claim[] claims = {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangePassword(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            return View(userEntity);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([Bind("UserId,LoginId,UserName,Password,SelfIntroduction,Icon")] User user)
        {
            var saltBytes = Password.CreateSalt(Password.saltSize);
            var hashBytes = Password.CreatePBKDF2Hash(user.Password, saltBytes, Password.hashSize, Password.iteration);
            user.Salt = Password.ChangeToBase64(saltBytes);
            user.Password = Password.ChangeToBase64(hashBytes);

            _context.Update(user);
            await _context.SaveChangesAsync();
            return View("Details",user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
