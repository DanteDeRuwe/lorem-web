using G09projectenII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace G09projectenII.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISessionRepository _sessionRepository;
        public HomeController(ISessionRepository sessionRepository) => _sessionRepository = sessionRepository;

        public IActionResult Index() => Content(string.Join("\n", _sessionRepository.GetAll().Select(s => s.Title)));//View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
