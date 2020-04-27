using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
using G09projectenII.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace G09projectenII.ViewComponents
{
    public class ProfilePicViewComponent : ViewComponent
    {
        private readonly IMemberRepository _memberRepository;

        public ProfilePicViewComponent(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IViewComponentResult Invoke()
        {
            Member member = _memberRepository.GetAll().FirstOrDefault(m => m.Username == User.Identity.Name);
            string profilePic = string.IsNullOrEmpty(member.Profilepicpath) ?
                "https://upload.wikimedia.org/wikipedia/commons/7/7c/Profile_avatar_placeholder_large.png" : member.Profilepicpath;
            return View(new ProfilePicViewModel { ProfilePicPath = profilePic });
        }
    }
}
