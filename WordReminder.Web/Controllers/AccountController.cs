using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordReminder.Biz.UnitOfWorks;
using WordReminder.Data.Model;
using WordReminder.Web.Models;

namespace WordReminder.Web.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUnitOfWork uow;
        public AccountController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userRpository = uow.GetRepository<WordReminder.Data.Model.User>();
            var user = await userRpository.GetAsync(q => q.Password == model.Password && q.Email == model.Email && q.IsActive == true);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Name,user.Fullname),
                    new Claim("UserId",user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,user.UserType.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                 await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = model.Remember,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
                    });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}