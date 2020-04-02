using G09projectenII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace G09projectenII.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionCalendarRepository _sessionCalendarRepository;


        public HomeController(ISessionRepository sessionRepository, ISessionCalendarRepository sessionCalendarRepository)
        {
            _sessionRepository = sessionRepository;
            _sessionCalendarRepository = sessionCalendarRepository;
        }

        public IActionResult Index() => Content(string.Join("\n\n---\n\n", _sessionRepository.GetBy(_sessionCalendarRepository.GetAll().First()).Select(s => s.Title)));

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
