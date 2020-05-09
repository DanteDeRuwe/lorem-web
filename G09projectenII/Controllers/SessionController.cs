using System;
using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
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


            return View(session);
        }

        [HttpPost]
        // TODO: move this to sessionController + code is untested for now
        public ActionResult RegisterUser(int sessionId)
        {
            Member member = _memberRepository.GetBy(HttpContext.User.Identity.Name);
            Session session = this._sessionRepository.GetBy(sessionId);
            session.RegisterUser(member);
            
            // this saves changes to the entire db context, so it is unnecessary
            // to run saveChanges on the memberRepository too
            _sessionRepository.SaveChanges();
            return RedirectToAction("Index", new {id = sessionId});
        }

        [HttpPost]
        public ActionResult UnregisterUser(int sessionId)
        {
            Member member = _memberRepository.GetBy(HttpContext.User.Identity.Name);
            Session session = this._sessionRepository.GetBy(sessionId);
            session.UnregisterUser(member);
            _sessionRepository.SaveChanges();
            return RedirectToAction("Index", new {id = sessionId});
        }
    }
}