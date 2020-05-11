using G09projectenII.Models.Repository_Models;
using G09projectenII.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Security.Claims;
using BCr = BCrypt.Net.BCrypt;

namespace G09projectenII.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMemberRepository _memberRepository;

        public AccountController(IMemberRepository memberRepository) => _memberRepository = memberRepository;

        public IActionResult Index() => User.Identity.IsAuthenticated ? (IActionResult)RedirectToAction("Index", "Calendar") : View();

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                if (_memberRepository.GetAll().Any(m => m.Username == login.Username
                && BCr.Verify(login.Password, m.Password)))
                {
                    var user = _memberRepository.GetAll().FirstOrDefault(m => m.Username == login.Username);

                    // check whether the user is blocked
                    if (user.Memberstatus == 2)
                    {
                        ModelState.AddModelError("failed-login", "Geblokkeerd account");
                        return View("index");
                    }

                    string role = user.Membertype switch
                    {
                        0 => "User",
                        1 => "Admin",
                        2 => "HeadAdmin",
                        _ => "User"
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(role + " Identity");

                    identity.AddClaim(new Claim(ClaimTypes.Name, login.Username));
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = login.RememberMe ? DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddHours(1)
                    });
                    return RedirectToAction("Index", "Calendar");
                }
                ModelState.AddModelError("failed-login", "Gebruikersnaam of wachtwoord onjuist");
            }
            return View("index");
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Calendar");
        }

        public ActionResult AccessDenied() => View("AccessDenied");
    }
}