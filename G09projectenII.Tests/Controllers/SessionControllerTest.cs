using G09projectenII.Controllers;
using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
using G09projectenII.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace G09projectenII.Tests.Controllers
{
    public class SessionControllerTest
    {

        private readonly DummyAppDbContext _dummyContext;
        private readonly SessionController _sessionController;
        private readonly ControllerContext _controllerContext;
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly Mock<HttpContext> _context;

        public SessionControllerTest()
        {
            _dummyContext = new DummyAppDbContext();
            _sessionRepository = new Mock<ISessionRepository>();
            _memberRepository = new Mock<IMemberRepository>();
            _context = new Mock<HttpContext>();
            _controllerContext = new ControllerContext()
            {
                HttpContext = _context.Object
            };
            _sessionController = new SessionController(_sessionRepository.Object, _memberRepository.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        #region --Index--
        [Fact]
        public void Index_SessionDoesNotExist_MemberExists_ReturnsNotFoundView()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns((Session) null);
            _context.SetupGet(c => c.User.Identity.Name).Returns("test");
            _memberRepository.Setup(mr => mr.GetBy("test")).Returns(_dummyContext.LoginTest);

            var result = Assert.IsType<NotFoundResult>(_sessionController.Index(1));
        }

        [Fact]
        public void Index_SessionExists_MemberDoesNotExist_ReturnsIndexView()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns(_dummyContext.SessionTest);
            _context.SetupGet(c => c.User.Identity.Name).Returns("test");
            _memberRepository.Setup(mr => mr.GetBy("test")).Returns((Member) null);

            var result = Assert.IsType<ViewResult>(_sessionController.Index(1));
            var session = Assert.IsType<Session>(result.Model);

            Assert.Equal(session, _dummyContext.SessionTest);
        }

        [Fact]
        public void Index_SessionAndMemberExist_ReturnsIndexView()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns(_dummyContext.SessionTest);
            _context.SetupGet(c => c.User.Identity.Name).Returns("test");
            _memberRepository.Setup(mr => mr.GetBy("test")).Returns(_dummyContext.LoginTest);

            var result = Assert.IsType<ViewResult>(_sessionController.Index(1));
            var session = Assert.IsType<Session>(result.Model);

            Assert.Equal(session, _dummyContext.SessionTest);
        }
        #endregion

        #region --ToggleRegistration Post--
        [Fact]
        public void ToggleRegistration_RedirectsToIndexWithIdView()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns(_dummyContext.SessionTest);
            _context.SetupGet(c => c.User.Identity.Name).Returns("test");
            _memberRepository.Setup(mr => mr.GetBy("test")).Returns(_dummyContext.LoginTest);

            var result = Assert.IsType<RedirectToActionResult>(_sessionController.ToggleRegistration(1));

            _sessionRepository.Verify(sr => sr.SaveChanges(), Times.Once());
        }
        #endregion
    }
}
