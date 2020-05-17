using G09projectenII.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace G09projectenII.Tests.Models
{
    public class SessionRegistreesTest
    {
        [Fact]
        public void NewSessionRegistreesWithParameters_InitiatesAllPropertiesCorrectly()
        {
            Session session = new Session();
            session.Id = 1;
            Member member = new Member();
            member.MemberId = 1;
            SessionRegistrees sr = new SessionRegistrees(session, member);

            Assert.Equal(session, sr.Session);
            Assert.Equal(1, sr.Id);
            Assert.Equal(member, sr.Member);
            Assert.Equal(1, sr.MemberId);
        }
    }
}
