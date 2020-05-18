using G09projectenII.Controllers;
using G09projectenII.Models.Repository_Models;
using G09projectenII.Models.ViewModels;
using G09projectenII.Tests.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace G09projectenII.Tests.Controllers
{
    public class AccountControllerTest
    {

        private readonly AccountController _accountController;
        private readonly DummyAppDbContext _dummyContext;
        private readonly Mock<IMemberRepository> _memberRepository;
        private readonly Mock<HttpContext> _context;
        private readonly Mock<IAuthenticationService> _aService;
        private readonly Mock<IServiceProvider> _sProvider;
        private readonly ControllerContext _controllerContext;
        private readonly HttpContext _defaultContext;

        public AccountControllerTest()
        {
            _dummyContext = new DummyAppDbContext();
            _memberRepository = new Mock<IMemberRepository>();
            _accountController = new AccountController(_memberRepository.Object)
            {
                TempData = new Mock<ITempDataDictionary>().Object,
                Url = new Mock<IUrlHelper>().Object
            };
            _context = new Mock<HttpContext>();
            _aService = new Mock<IAuthenticationService>();
            _sProvider = new Mock<IServiceProvider>();
            _defaultContext = new DefaultHttpContext()
            {
                RequestServices = _sProvider.Object
            };
            _controllerContext = new ControllerContext()
            {
                HttpContext = _context.Object
            };
        }

        #region --Index--
        [Fact]
        public void Index_UserIsAuthenticated_RedirectsToCalendarIndexView()
        {
            _context.Setup(u => u.User.Identity.IsAuthenticated).Returns(true);
            _accountController.ControllerContext = _controllerContext;

            var result = Assert.IsType<RedirectToActionResult>(_accountController.Index());
        }

        [Fact]
        public void Index_UserIsNotAuthenticated_GetsView()
        {
            _context.Setup(u => u.User.Identity.IsAuthenticated).Returns(false);
            _accountController.ControllerContext = _controllerContext;

            var result = Assert.IsType<ViewResult>(_accountController.Index());
        }
        #endregion

        #region --Login Post--
        [Fact]
        public void Login_ValidLogin_RedirectsToCalendarIndexView()
        {
            _memberRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Members);
            _aService.Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                     .Returns(Task.FromResult((object)null));
            _sProvider.Setup(_ => _.GetService(typeof(IAuthenticationService)))
                      .Returns(_aService.Object);
            _accountController.ControllerContext = new ControllerContext()
            {
                HttpContext = _defaultContext
            };
            LoginViewModel loginVM = new LoginViewModel()
            {
                Username = "test",
                Password = "test"
            };

            var result = Assert.IsType<RedirectToActionResult>(_accountController.Login(loginVM));
            Assert.Equal("Index", result?.ActionName);
        }

        [Fact]
        public void Login_NonValidLogin_ReturnsIndexView()
        {
            _memberRepository.Setup(m => m.GetAll()).Returns(_dummyContext.Members);

            LoginViewModel loginVM = new LoginViewModel() { Username = "arne", Password = "test" };

            var result = Assert.IsType<ViewResult>(_accountController.Login(loginVM));
        }
        #endregion

        #region --Logout--
        [Fact]
        public void LogOut_RedirectsToCalendarIndexView()
        {
            _aService.Setup(_ => _.SignOutAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<AuthenticationProperties>())).Returns(Task.FromResult((object)null));
            _sProvider.Setup(_ => _.GetService(typeof(IAuthenticationService))).Returns(_aService.Object);
            _accountController.ControllerContext = new ControllerContext() { HttpContext = _defaultContext };

            var result = Assert.IsType<RedirectToActionResult>(_accountController.Logout());
        }
        #endregion

        #region --AccessDenied--
        [Fact]
        public void AccessDenied_ReturnsAccessDeniedView()
        {
            var result = Assert.IsType<ViewResult>(_accountController.AccessDenied());
        }
        #endregion
    }
}
