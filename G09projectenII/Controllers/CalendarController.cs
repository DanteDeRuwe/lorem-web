using G09projectenII.Models;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace G09projectenII.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public CalendarController(ISessionRepository sessionRepository) => _sessionRepository = sessionRepository;

        public IActionResult Index() => View(_sessionRepository.GetAll());

        [HttpPost]
        public JsonResult GetSessionBy(int id) => Json(this._sessionRepository.GetBy(id));

        [HttpGet]
        public JsonResult Test(int id)
        {
            return Json(id);
        }
    }
}