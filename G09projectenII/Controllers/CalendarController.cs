using G09projectenII.Models;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace G09projectenII.Controllers
{
    public class CalendarController : Controller
    {

        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionCalendarRepository _sessionCalendarRepository;

        public CalendarController(ISessionRepository sessionRepository, ISessionCalendarRepository sessionCalendarRepository)
        {
            _sessionRepository = sessionRepository;
            _sessionCalendarRepository = sessionCalendarRepository;
        }

        public IActionResult Index() => View(_sessionCalendarRepository.GetBy(3));
        
        
    }
}