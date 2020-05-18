using G09projectenII.Models;
using Xunit;

namespace G09projectenII.Tests.Models
{
    public class SessionAttendeesTest
    {
        [Fact]
        public void NewSessionRegistreesWithParameters_InitiatesAllPropertiesCorrectly()
        {
            Session session = new Session
            {
                Id = 1
            };
            Member member = new Member
            {
                MemberId = 1
            };
            SessionRegistrees sr = new SessionRegistrees(session, member);

            Assert.Equal(session, sr.Session);
            Assert.Equal(1, sr.Id);
            Assert.Equal(member, sr.Member);
            Assert.Equal(1, sr.MemberId);
        }
    }
}
