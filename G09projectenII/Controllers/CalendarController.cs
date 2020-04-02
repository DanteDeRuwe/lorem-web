using Microsoft.AspNetCore.Mvc;

namespace G09projectenII.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}