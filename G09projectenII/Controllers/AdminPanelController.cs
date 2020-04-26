using G09projectenII.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace G09projectenII.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public AdminPanelController(ISessionRepository sessionRepository) => _sessionRepository = sessionRepository;

        public IActionResult Index()
        {
            List<Session> nonFinishedSessions =
                _sessionRepository.GetAll().Where(s => s.SessionState.toInt() != 3).OrderBy(s => s.StartDateTime).ToList();
            return View(nonFinishedSessions);
        }
    }
}