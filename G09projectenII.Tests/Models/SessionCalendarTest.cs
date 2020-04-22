using G09projectenII.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
