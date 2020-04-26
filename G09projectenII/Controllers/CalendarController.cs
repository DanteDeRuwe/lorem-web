using G09projectenII.Models;
using G09projectenII.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace G09projectenII.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public CalendarController(ISessionRepository sessionRepository) => _sessionRepository = sessionRepository;

        [Authorize]
        public IActionResult Index() => View(_sessionRepository.GetAll());

        [HttpPost]
        public JsonResult GetSessionBy(int id) => Json(this._sessionRepository.GetBy(id));

        [HttpGet]
        public JsonResult Test(int id)
        {
            Session session = this._sessionRepository.GetBy(id);
            return session != null ? Json(new SessionDTO(session)) : null;
        }
    }
}