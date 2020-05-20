using G09projectenII.Models;
using G09projectenII.Models.DTOs;
using G09projectenII.Models.Repository_Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace G09projectenII.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMemberRepository _memberRepository;

        public CalendarController(ISessionRepository sessionRepository, IMemberRepository memberRepository)
        {
            _sessionRepository = sessionRepository;
            _memberRepository = memberRepository;
        }

        public IActionResult Index() => View(_sessionRepository.GetAll());

        [HttpGet]
        public JsonResult GetSessionBy(int id)
        {
            var session = _sessionRepository.GetBy(id);
            return session != null ? Json(new SessionDTO(session)) : null;
        }

        [HttpGet]
        public JsonResult GetRegistrationStatus(int sessionId)
        {
            var member = _memberRepository.GetBy(HttpContext.User.Identity.Name);
            var session = _sessionRepository.GetBy(sessionId);

            if (member == null) return Json("");

            bool hasAttended = session.SessionAttendees.Any(sa => sa.MemberId == member.MemberId);
            bool isRegistered = session.SessionRegistrees.Any(sr => sr.MemberId == member.MemberId);

            return Json(new RegistrationStatusDTO() { HasAttended = hasAttended, IsRegistered = isRegistered });
        }
    }
}