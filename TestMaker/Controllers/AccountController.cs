using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMaker.Data;
using TestMaker.Models;

namespace TestMaker.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestMakerContext _context;

        public AccountController(TestMakerContext context)
        {
            _context = context;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("LoginId,Password")] User loginUser, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var userInfo = _context.Users.SingleOrDefault(o => o.LoginId == loginUser.LoginId);
                var saltBytes = Password.ChangeFromBase64(userInfo.Salt);
                var inputHashBytes = Password.CreatePBKDF2Hash(loginUser.Password, saltBytes, Password.hashSize, Password.iteration);
                var inputHashText = Password.ChangeToBase64(inputHashBytes);
                if (userInfo.Password == inputHashText)
                {
                    Claim[] claims = {
                        new Claim(ClaimTypes.NameIdentifier, userInfo.UserId.ToString()),
                        new Claim(ClaimTypes.Name, userInfo.UserName),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    return LocalRedirect(returnUrl ?? Url.Content("~/"));
                }
                else
                {
                    return View(loginUser);
                }
            }
            return View(loginUser);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}
