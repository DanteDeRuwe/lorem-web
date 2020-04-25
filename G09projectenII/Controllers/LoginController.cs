using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
using G09projectenII.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Security.Claims;

namespace G09projectenII.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMemberRepository _memberRepository;

        public LoginController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid && _memberRepository.GetAll().Any(m => m.Username == login.Username)) //TODO: password hash/check
            {
                ClaimsIdentity identity = new ClaimsIdentity("User Identity");
                identity.AddClaim(new Claim(ClaimTypes.Name, login.Username));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Calendar");
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(login);
        }
    }
}