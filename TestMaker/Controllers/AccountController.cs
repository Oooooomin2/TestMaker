using System.Security.Claims;
using System.Threading.Tasks;
using DDD.Domain.Model.Interface;
using DDD.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestMaker.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("LoginId,Password")] Login loginUser, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var userInfo = _accountRepository.GetSelectedUser(loginUser);
                if(userInfo == null)
                {
                    ModelState.AddModelError("LoginId", "The LoginId is unregistered");
                    return View(loginUser);
                }
                var inputHashText = Password.CreateHashTextBase64(userInfo.Salt, loginUser.Password);
                if (userInfo.Password == inputHashText)
                {
                    var claimsIdentity = new Authenticate(
                        userId: userInfo.UserId,
                        userName: userInfo.UserName).CreateClaimsIdentity();
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    return LocalRedirect(returnUrl ?? Url.Content("~/"));
                }
                else
                {
                    ModelState.AddModelError("Password", "The Password is incorrect.");
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
