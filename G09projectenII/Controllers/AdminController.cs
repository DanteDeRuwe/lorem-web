using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
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
        private readonly IMemberRepository _memberRepository;

        public AdminController(ISessionRepository sessionRepository, IMemberRepository memberRepository)
        {
            _sessionRepository = sessionRepository;
            _memberRepository = memberRepository;
        }

        public IActionResult Index() => RedirectToAction("Sessions");

        public IActionResult Sessions()
        {
            List<Session> nonFinishedSessions =
                _sessionRepository.GetAll()
                    .Where(s => !(s.SessionState is FinishedSessionState))
                    .Where(s => s.Member.Username.Equals(User.Identity.Name) || User.IsInRole("HeadAdmin"))
                    .OrderBy(s => s.StartDateTime)
                    .ToList();

            return View(nonFinishedSessions);
        }

        public IActionResult Attendances()
        {
            List<Session> closedSessions =
                _sessionRepository.GetAll()
                    .Where(s => s.SessionState is ClosedSessionState)
                    .Where(s => s.Member.Username.Equals(User.Identity.Name) || User.IsInRole("HeadAdmin"))
                    .OrderBy(s => s.StartDateTime)
                    .ToList();

            return View(closedSessions);
        }

        [Route("Admin/Session/{id}/Attendances")]
        public IActionResult SessionAttendances(int id)
        {
            var session = _sessionRepository.GetBy(id);
            return session == null ? (IActionResult)NotFound() : View(session);
        }


        [HttpPost]
        public IActionResult ChangeSessionState(int id)
        {
            var session = _sessionRepository.GetBy(id);
            session.NextState();
            _sessionRepository.Update(session);
            _sessionRepository.SaveChanges();

            return RedirectToAction("Sessions");
        }

        [HttpPost]
        public IActionResult AttendUser(int sessionId, int memberId)
        {
            var session = _sessionRepository.GetBy(sessionId);
            var member = _memberRepository.GetBy(memberId);
            session.AttendUser(member);

            _memberRepository.SaveChanges();
            _sessionRepository.SaveChanges();

            return RedirectToAction("SessionAttendances", new { id = sessionId });
        }

    }
}