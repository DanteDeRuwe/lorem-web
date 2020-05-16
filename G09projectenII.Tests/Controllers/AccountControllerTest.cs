using G09projectenII.Controllers;
using G09projectenII.Models.Repository_Models;
using G09projectenII.Tests.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using G09projectenII.Models;
using G09projectenII.Models.ViewModels;

namespace G09projectenII.Tests.Controllers
{
    public class AccountControllerTest
    {

        private readonly AccountController _accountController;
        private readonly CalendarController _calendarController;
        private readonly DummyAppDbContext _dummyContext;
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly Mock<ISessionRepository> _sessionRepository;
        private readonly Mock<HttpContext> _context;

        public AccountControllerTest()
        {
            _dummyContext = new DummyAppDbContext();
            _memberRepository = new Mock<IMemberRepository>();
            _sessionRepository = new Mock<ISessionRepository>();
            _accountController = new AccountController(_memberRepository.Object);
            _calendarController = new CalendarController(_sessionRepository.Object, _memberRepository.Object);
            _context = new Mock<HttpContext>();
        }

        #region --Index--
        [Fact]
        public void Index_UserIsAuthenticated_RedirectsToCalendarIndexView()
        {
            _context.Setup(u => u.User.Identity.IsAuthenticated).Returns(true);
            var result = Assert.IsType<ViewResult>(_calendarController.Index());
        }

        [Fact]
        public void Index_UserIsNotAuthenticated_GetsView()
        {
            _context.Setup(u => u.User.Identity.IsAuthenticated).Returns(false);
            var result = Assert.IsType<ViewResult>(_accountController.Index());
        }
        #endregion

        #region --Login Post--
        [Fact]
        public void Login_ValidLogin_RedirectsToCalendarIndexView()
        {
            _memberRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Members);
            var loginVM = new LoginViewModel()
            {
                Username = "test",
                Password = "test"
            };
            var result = Assert.IsType<RedirectToActionResult>(_accountController.Login(loginVM));
            Assert.Equal("Index", result?.ActionName);
        }
        #endregion
    }
}
