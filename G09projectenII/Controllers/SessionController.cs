using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index(int id) => View(_sessionRepository.GetBy(id));
    }
}