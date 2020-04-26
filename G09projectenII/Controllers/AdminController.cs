using G09projectenII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace G09projectenII.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public AdminController(ISessionRepository sessionRepository) => _sessionRepository = sessionRepository;

        public IActionResult Index() => RedirectToAction("Sessions");

        public IActionResult Sessions()
        {
            List<Session> nonFinishedSessions =
                _sessionRepository.GetAll()
                    .Where(s => s.SessionState.ToInt() != 3)
                    .OrderBy(s => s.StartDateTime)
                    .ToList();

            return View(nonFinishedSessions);
        }

        public IActionResult Attendances() => View();
    }
}