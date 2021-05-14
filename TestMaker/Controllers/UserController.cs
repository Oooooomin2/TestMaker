using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DDD.Domain.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMaker.Data;
using TestMaker.Models;
using TestMaker.Models.Interface;

namespace TestMaker.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Users
        public IActionResult Index()
        {
            ViewData["Title"] = "Index";
            ViewData["Action"] = "Index";
            ViewData["Controller"] = "User";
            return View(_userRepository.GetAll());
        }

        // GET: Users/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "User's information";
            ViewData["Action"] = "Details";
            ViewData["Controller"] = "User";
            return View(_userRepository.GetContent(id));
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
        public IActionResult Create([Bind("UserId,LoginId,UserName,Password,ConfirmPassword")] User user)
        {
            if(_userRepository.LoginIdExists(user))
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
                _userRepository.Create(user);
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
            return View(_userRepository.FindUser(id));
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserId,LoginId,Password,ConfirmPassword,Salt,UserName,SelfIntroduction,Icon")] User user)
        {
            if (id != user.UserId) return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                using var memoryStream = new MemoryStream();
                files.SingleOrDefault().CopyTo(memoryStream);
                user.Icon = memoryStream.ToArray();
            }

            try
            {
                _userRepository.Update(user);
                if (User.FindFirst(ClaimTypes.Name).Value != user.UserName)
                {
                    var claimsIdentity = new Authenticate(
                        userId: user.UserId,
                        userName: user.UserName).CreateClaimsIdentity();
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userRepository.UserExists(user.UserId))
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
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Delete";
            ViewData["Action"] = "Delete";
            ViewData["Controller"] = "User";
            return View(_userRepository.GetContent(id));
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
             _userRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePassword(int id)
        {
            ViewData["Title"] = "Change password";
            ViewData["Action"] = "ChangePassword";
            ViewData["Controller"] = "User";
            return View(_userRepository.FindUser(id));
        }

        [HttpPost]
        public IActionResult ChangePassword([Bind("UserId,LoginId,UserName,Password,ConfirmPassword,SelfIntroduction,Icon")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Salt = Password.CreateSaltBase64();
                user.Password = Password.CreatePasswordHashBase64(Convert.FromBase64String(user.Salt), user.Password);
                _userRepository.Update(user);
                ViewData["Title"] = "User's information";
                ViewData["Action"] = "Details";
                ViewData["Controller"] = "User";
                return View("Details", user);
            }
            ViewData["Title"] = "Change password";
            ViewData["Action"] = "ChangePassword";
            ViewData["Controller"] = "User";
            return View(user);
        }
    }
}
