using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G09projectenII.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace G09projectenII.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPage(LoginViewModel login)
        {

            return View(login);
        }
    }
}