using G09projectenII.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace G09projectenII.Controllers
{
    [Authorize(Policy = "AdminOnly")]
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


        [HttpPost]
        public IActionResult ChangeSessionState(int id)
        {
            var session = _sessionRepository.GetBy(id);
            session.NextState();
            _sessionRepository.Update(session);
            _sessionRepository.SaveChanges();

            return RedirectToAction("Sessions");
        }


    }
}