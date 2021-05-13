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
            ViewData["Title"] = "Index";
            ViewData["Action"] = "Index";
            ViewData["Controller"] = "User";
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "User's information";
            ViewData["Action"] = "Details";
            ViewData["Controller"] = "User";
            return View(await _context.Users.FirstOrDefaultAsync(m => m.UserId == id));
        }

        [AllowAnonymous]
        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Create";
            ViewData["Action"] = "Create";
            ViewData["Controller"] = "User";
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,LoginId,UserName,Password,ConfirmPassword")] User user)
        {
            if(_context.Users.Where(o => o.LoginId == user.LoginId).Any())
            {
                ViewData["Title"] = "Create";
                ViewData["Action"] = "Create";
                ViewData["Controller"] = "User";
                ModelState.AddModelError("LoginId", "The LoginId is unregistered");
                return View(user);
            }
            if (ModelState.IsValid)
            {
                user.Salt = Password.CreateSaltBase64();
                user.Password = Password.CreatePasswordHashBase64(Convert.FromBase64String(user.Salt), user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            ViewData["Title"] = "Create";
            ViewData["Action"] = "Create";
            ViewData["Controller"] = "User";
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Edit";
            ViewData["Action"] = "Edit";
            ViewData["Controller"] = "User";
            return View(_context.Users.Find(id));
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,LoginId,Password,ConfirmPassword,Salt,UserName,SelfIntroduction,Icon")] User user)
        {
            if (id != user.UserId) return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                using var memoryStream = new MemoryStream();
                await files.SingleOrDefault().CopyToAsync(memoryStream);
                user.Icon = memoryStream.ToArray();
            }

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                if (User.FindFirst(ClaimTypes.Name).Value != user.UserName)
                {
                    var claimsIdentity = new Authenticate(
                        userId: user.UserId,
                        userName: user.UserName).CreateClaimsIdentity();
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
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Delete";
            ViewData["Action"] = "Delete";
            ViewData["Controller"] = "User";
            return View(await _context.Users.FirstOrDefaultAsync(m => m.UserId == id));
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ChangePassword(int id)
        {
            return View(await _context.Users.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([Bind("UserId,LoginId,UserName,Password,ConfirmPassword,SelfIntroduction,Icon")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Salt = Password.CreateSaltBase64();
                user.Password = Password.CreatePasswordHashBase64(Convert.FromBase64String(user.Salt), user.Password);
                _context.Update(user);
                await _context.SaveChangesAsync();
                return View("Details", user);
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
