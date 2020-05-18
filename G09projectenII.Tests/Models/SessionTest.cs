using G09projectenII.Models;
using Xunit;

namespace G09projectenII.Tests.Models
{
    public class SessionTest
    {

        [Fact]
        public void NewSession_CreatesEmptyAnnouncementsSessionRegistreesAndAttendeesLists()
        {
            Session session = new Session();

            Assert.Empty(session.Announcements);
            Assert.Empty(session.SessionAttendees);
            Assert.Empty(session.SessionRegistrees);
        }

        [Fact]
        public void NumberOfRegistrees_ReturnsAmountOfSessionRegistrees()
        {
            Session session = new Session();

            Assert.Equal(0, session.NumberOfRegistrees);
        }

        [Fact]
        public void NumberOfAttendees_ReturnsAmountOfSessionAttendees()
        {
            Session session = new Session();

            Assert.Equal(0, session.NumberOfAttendees);
        }

        [Fact]
        public void AvailableRegistrationSpots_ReturnsCapacityMinusAmountOfRegistrees()
        {
            Session session = new Session
            {
                Capacity = 2
            };

            Assert.Equal(2, session.AvailableRegistrationSpots);
        }

        [Fact]
        public void HasStarted_SessionHasStarted_ReturnsTrue()
        {
            Session session = new Session
            {
                SessionState = new ClosedSessionState()
            };

            Assert.True(session.HasStarted);
        }

        [Fact]
        public void HasStarted_SessionHasNotStarted_ReturnsFalse()
        {
            Session session = new Session
            {
                SessionState = new OpenSessionState()
            };

            Assert.False(session.HasStarted);
        }

        [Fact]
        public void IsOpen_SessionIsOpen_ReturnsTrue()
        {
            Session session = new Session
            {
                SessionState = new OpenSessionState()
            };

            Assert.True(session.IsOpen);
        }

        [Fact]
        public void IsOpen_SessionIsNotOpen_ReturnsFalse()
        {
            Session session = new Session
            {
                SessionState = new CreatedSessionState()
            };

            Assert.False(session.IsOpen);
        }

        [Fact]
        public void NextState_SessionStateSwitchesToTheNextState()
        {
            Session session = new Session
            {
                SessionState = new CreatedSessionState()
            };

            session.NextState();
            Assert.Equal(1, session.SessionState.ToInt());

            session.NextState();
            Assert.Equal(2, session.SessionState.ToInt());

            session.NextState();
            Assert.Equal(3, session.SessionState.ToInt());

            session.NextState();
            Assert.Equal(3, session.SessionState.ToInt());
        }

        [Fact]
        public void RegisterUser_SessionIsNotOpen_DoesNotRegisterMember()
        {
            Session session = new Session
            {
                SessionState = new CreatedSessionState()
            };
            session.RegisterUser(new Member());

            Assert.Equal(0, session.NumberOfRegistrees);
        }

        [Fact]
        public void RegisterUser_NoAvailableSpots_DoesNotRegisterMember()
        {
            Session session = new Session
            {
                SessionState = new OpenSessionState()
            };
            session.RegisterUser(new Member());

            Assert.Equal(0, session.NumberOfRegistrees);
        }

        [Fact]
        public void RegisterUser_ValidRegistration_RegistersMemberCorrectly()
        {
            Session session = new Session
            {
                SessionState = new OpenSessionState(),
                Capacity = 1
            };
            session.RegisterUser(new Member());

            Assert.Equal(1, session.NumberOfRegistrees);
        }

        [Fact]
        public void AttendUser_AttendsMemberCorrectly()
        {
            Session session = new Session();
            session.AttendUser(new Member());

            Assert.Equal(1, session.NumberOfAttendees);
        }
    }
}
