

using G09projectenII.Controllers;
using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
using G09projectenII.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace G09projectenII.Tests.Controllers
{
    public class AdminControllerTest
    {

        private readonly DummyAppDbContext _dummyContext;
        private readonly AdminController _adminController;
        private readonly ControllerContext _controllerContext;
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly Mock<HttpContext> _context;

        public AdminControllerTest()
        {
            _dummyContext = new DummyAppDbContext();
            _sessionRepository = new Mock<ISessionRepository>();
            _memberRepository = new Mock<IMemberRepository>();
            _context = new Mock<HttpContext>();
            _controllerContext = new ControllerContext() { HttpContext = _context.Object };
            _adminController = new AdminController(_sessionRepository.Object, _memberRepository.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        #region --Index--
        [Fact]
        public void Index_RedirectsToSessionsView()
        {
            var result = Assert.IsType<RedirectToActionResult>(_adminController.Index());
        }
        #endregion

        #region --Sessions--
        [Fact]
        public void Sessions_PassesAllFinishedSessionsToView()
        {
            _sessionRepository.Setup(sr => sr.GetAll()).Returns(_dummyContext.Sessions);
            _context.SetupGet(c => c.User.Identity.Name).Returns("test");
            _context.Setup(c => c.User.IsInRole("HeadAdmin")).Returns(true);

            var result = Assert.IsType<ViewResult>(_adminController.Sessions());
            var sessions = Assert.IsType<List<Session>>(result.Model);

            Assert.Equal(sessions, _dummyContext.Sessions);
        }
        #endregion

        #region --Attendances--
        [Fact]
        public void Attendances_PassesAllOpenSessionsToView()
        {
            _sessionRepository.Setup(sr => sr.GetAll()).Returns(_dummyContext.Sessions);
            _context.SetupGet(c => c.User.Identity.Name).Returns("test");
            _context.Setup(c => c.User.IsInRole("HeadAdmin")).Returns(true);

            var result = Assert.IsType<ViewResult>(_adminController.Attendances());
            var sessions = Assert.IsType<List<Session>>(result.Model);

            Assert.Empty(sessions);
        }
        #endregion

        #region --SessionAttendances--
        [Fact]
        public void SessionAttendances_SessionExists_PassesSessionToView()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns(_dummyContext.SessionTest);

            var result = Assert.IsType<ViewResult>(_adminController.SessionAttendances(1));
            var session = Assert.IsType<Session>(result.Model);

            Assert.Equal(session, _dummyContext.SessionTest);
        }

        [Fact]
        public void SessionAttendances_SessionDoesNotExist_ReturnsNotFoundView()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns((Session)null);

            var result = Assert.IsType<NotFoundResult>(_adminController.SessionAttendances(1));
        }
        #endregion

        #region --ChangeSessionState Post--
        [Fact]
        public void ChangeSessionState_RedirectsToSessionsView()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns(_dummyContext.SessionTest);

            var result = Assert.IsType<RedirectToActionResult>(_adminController.ChangeSessionState(1));

            _sessionRepository.Verify(sr => sr.Update(_dummyContext.SessionTest), Times.Once());
            _sessionRepository.Verify(sr => sr.SaveChanges(), Times.Once());
        }
        #endregion

        #region --AttendUser--
        [Fact]
        public void AttendUser_RedirectsToSessionAttendancesWithIdView()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns(_dummyContext.SessionTest);
            _memberRepository.Setup(mr => mr.GetBy(1)).Returns(_dummyContext.LoginTest);

            var result = Assert.IsType<RedirectToActionResult>(_adminController.AttendUser(1, 1));

            _sessionRepository.Verify(sr => sr.SaveChanges(), Times.Once());
            _memberRepository.Verify(mr => mr.SaveChanges(), Times.Once());
        }
        #endregion
    }
}
