using G09projectenII.Controllers;
using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
using G09projectenII.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace G09projectenII.Tests.Controllers
{
    public class CalendarControllerTest
    {
        private readonly DummyAppDbContext _dummyContext;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly Mock<HttpContext> _context;
        private readonly ControllerContext _controllerContext;
        private readonly CalendarController _calendarController;

        public CalendarControllerTest()
        {
            _dummyContext = new DummyAppDbContext();
            _sessionRepository = new Mock<ISessionRepository>();
            _memberRepository = new Mock<IMemberRepository>();
            _context = new Mock<HttpContext>();
            _controllerContext = new ControllerContext() 
            {
                HttpContext = _context.Object
            };
            _calendarController = new CalendarController(_sessionRepository.Object, _memberRepository.Object)
            {
                ControllerContext = _controllerContext
            };
        }

        #region --Index--
        [Fact]
        public void Index_GetsAllSessionsAndPassesThemToView()
        {
            _sessionRepository.Setup(sr => sr.GetAll()).Returns(_dummyContext.Sessions);
            var result = Assert.IsType<ViewResult>(_calendarController.Index());
            var sessionsInModel = Assert.IsType<List<Session>>(result.Model);
            Assert.Equal(1, sessionsInModel.Count);
            Assert.Equal("test session", sessionsInModel[0].Title);
        }
        #endregion

        #region --GetRegistrationStatus--
        [Fact]
        public void GetRegistrationStatus_MemberExists_ReturnsJsonResult()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns(_dummyContext.SessionTest);
            _context.SetupGet(c => c.User.Identity.Name).Returns("test");
            _memberRepository.Setup(mr => mr.GetBy("test")).Returns(_dummyContext.LoginTest);
            
            var result = Assert.IsType<JsonResult>(_calendarController.GetRegistrationStatus(1));
        }

        [Fact]
        public void GetRegistrationStatus_MemberDoesNotExist_ReturnsNull()
        {
            _context.SetupGet(c => c.User.Identity.Name).Returns("test");
            _memberRepository.Setup(mr => mr.GetBy("test")).Returns((Member) null);

            var result = Assert.IsType<JsonResult>(_calendarController.GetRegistrationStatus(1));
        }
        #endregion

        #region --GetSessionBy--
        [Fact]
        public void GetSessionBy_SessionExists_ReturnsJsonResult()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns(_dummyContext.SessionTest);

            var result = Assert.IsType<JsonResult>(_calendarController.GetSessionBy(1));
        }

        [Fact]
        public void GetSessionBy_SessionDoesNotExist_ReturnsJsonResult()
        {
            _sessionRepository.Setup(sr => sr.GetBy(1)).Returns((Session) null);

            Assert.Null(_calendarController.GetSessionBy(1));
        }
        #endregion
    }
}
