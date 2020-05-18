using G09projectenII.Models;
using Xunit;

namespace G09projectenII.Tests.Models
{
    public class MemberTest
    {
        [Fact]
        public void NewMember_EmptyAnnouncementsSessionsSessionAttendeesAndRegistreesLists()
        {
            Member member = new Member();

            Assert.Empty(member.Announcements);
            Assert.Empty(member.Sessions);
            Assert.Empty(member.SessionAttendees);
            Assert.Empty(member.SessionRegistrees);
        }

        [Fact]
        public void GetFullName_ReturnsFormattedString()
        {
            Member member = new Member
            {
                Firstname = "a",
                Lastname = "b"
            };

            Assert.Equal("a b", member.GetFullName());
        }
    }
}
