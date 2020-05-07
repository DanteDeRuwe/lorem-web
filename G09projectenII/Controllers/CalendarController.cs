using System;
using System.Linq;
using System.Runtime.InteropServices;
using G09projectenII.Models;
using G09projectenII.Models.DTOs;
using G09projectenII.Models.Repository_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
            Session session = this._sessionRepository.GetBy(id);
            return session != null ? Json(new SessionDTO(session)) : null;
        }

        [HttpGet]
        public JsonResult GetRegistrationStatus(int sessionId)
        {
            Member member = _memberRepository.GetBy(HttpContext.User.Identity.Name);
            Session session = this._sessionRepository.GetBy(sessionId);

            bool hasAttended = session.SessionAttendees.Any(sa => sa.MemberId == member.MemberId);
            bool isRegistered = session.SessionRegistrees.Any(sr => sr.MemberId == member.MemberId);

            return Json(new RegistrationStatusDTO() { HasAttended = hasAttended, IsRegistered = isRegistered });
        }

        [HttpPost]
        // TODO: move this to sessionController + code is untested for now
        public void RegisterUser(int sessionId)
        {
            Member member = _memberRepository.GetBy(HttpContext.User.Identity.Name);
            Session session = this._sessionRepository.GetBy(sessionId);
            session.registerUser(member);

            // this saves changes to the entire db context, so it is unnecessary
            // to run saveChanges on the memberRepository too
            _sessionRepository.SaveChanges();
        }
    }
}