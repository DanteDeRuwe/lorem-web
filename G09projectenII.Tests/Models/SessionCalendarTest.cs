using G09projectenII.Models;
using Xunit;

namespace G09projectenII.Tests.Models
{
    public class SessionCalendarTest
    {
        [Fact]
        public void NewSessionCalendar_EmptyListOfSessions()
        {
            SessionCalendar sessionCalendar = new SessionCalendar();

            Assert.Empty(sessionCalendar.Sessions);
        }

        [Fact]
        public void GetSessionBy_SessionExists_ReturnsSession()
        {
            Session session = new Session { Id = 1 };
            SessionCalendar sessionCalendar = new SessionCalendar() { Sessions = new[] { session } };

            Assert.Equal(session, sessionCalendar.GetSessionBy(1));
        }

        [Fact]
        public void GetSessionBy_SessionDoesNotExist_ReturnsNull()
        {
            SessionCalendar sessionCalendar = new SessionCalendar();

            Assert.Equal(null, sessionCalendar.GetSessionBy(1));
        }
    }
}
