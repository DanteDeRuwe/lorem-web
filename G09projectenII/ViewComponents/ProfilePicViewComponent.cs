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

        public ProfilePicViewComponent(IMemberRepository memberRepository) => _memberRepository = memberRepository;

        public IViewComponentResult Invoke(int size = 32, bool rounded = true, Member member = null, string cssClass = "")
        {
            // Use logged in member if no member is given
            if (member == null) member = _memberRepository.GetAll().FirstOrDefault(m => m.Username == User.Identity.Name);

            return View(new ProfilePicViewModel
            {
                Size = size,
                Rounded = rounded,
                CssClass = cssClass,
                // Use placeholder avatar if the member doesn't have a profile picture
                ProfilePicPath = string.IsNullOrEmpty(member?.Profilepicpath) ?
                "https://upload.wikimedia.org/wikipedia/commons/7/7c/Profile_avatar_placeholder_large.png" : member.Profilepicpath
            });
        }
    }
}
