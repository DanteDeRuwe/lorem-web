using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
using G09projectenII.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace G09projectenII.Controllers
{
    public class SessionController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ISessionRepository _sessionRepository;

        public SessionController(ISessionRepository sessionRepository, IMemberRepository memberRepository)
        {
            _sessionRepository = sessionRepository;
            _memberRepository = memberRepository;
        }


        [Route("Session/{id}")]
        public IActionResult Index(int id)
        {
            var session = _sessionRepository.GetBy(id);
            var member = _memberRepository.GetBy(HttpContext.User.Identity.Name);

            if (session == null) return NotFound();

            if (member != null)
            {
                ViewBag.MemberIsRegistered = session.SessionRegistrees
                    .Any(r => r.MemberId == member.MemberId);

                ViewBag.MemberHasAttended = session.SessionAttendees
                    .Any(a => a.MemberId == member.MemberId);
            }

            ViewBag.ExtraInfoText = Util.ExtraSessionInfo(session);

            return View(session);
        }

        [HttpPost]
        public ActionResult ToggleRegistration(int sessionId)
        {
            var member = _memberRepository.GetBy(HttpContext.User.Identity.Name);
            var session = _sessionRepository.GetBy(sessionId);
            if (session.SessionRegistrees.Any(r => r.MemberId == member.MemberId))
            {
                session.UnregisterUser(member);
            }
            else
            {
                session.RegisterUser(member);

            }

            _sessionRepository.SaveChanges();
            return RedirectToAction("Index", new { id = sessionId });
        }

    }
}