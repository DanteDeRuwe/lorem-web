﻿using G09projectenII.Models;
using System.Linq;
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
        public void HasStarted_SessionHasStarted_ReturnsTrue()
        {
            Session session = new Session { SessionState = new ClosedSessionState() };

            Assert.True(session.HasStarted);
        }

        [Fact]
        public void HasStarted_SessionHasNotStarted_ReturnsFalse()
        {
            Session session = new Session { SessionState = new OpenSessionState() };

            Assert.False(session.HasStarted);
        }

        [Fact]
        public void IsOpen_SessionIsOpen_ReturnsTrue()
        {
            Session session = new Session { SessionState = new OpenSessionState() };

            Assert.True(session.IsOpen);
        }

        [Fact]
        public void IsOpen_SessionIsNotOpen_ReturnsFalse()
        {
            Session session = new Session { SessionState = new CreatedSessionState() };

            Assert.False(session.IsOpen);
        }

        [Fact]
        public void NextState_SessionStateSwitchesToTheNextState()
        {
            Session session = new Session { SessionState = new CreatedSessionState() };

            session.NextState();
            Assert.IsType<OpenSessionState>(session.SessionState);

            session.NextState();
            Assert.IsType<ClosedSessionState>(session.SessionState);

            session.NextState();
            Assert.IsType<FinishedSessionState>(session.SessionState);

            session.NextState();
            Assert.IsType<FinishedSessionState>(session.SessionState); //stays finished
        }

        [Fact]
        public void RegisterUser_SessionIsNotOpen_DoesNotRegisterMember()
        {
            Session session = new Session { SessionState = new CreatedSessionState() };
            session.RegisterUser(new Member());

            Assert.Equal(0, session.NumberOfRegistrees);
        }

        [Fact]
        public void RegisterUser_NoAvailableSpots_DoesNotRegisterMember()
        {
            Session session = new Session { SessionState = new OpenSessionState() };
            session.RegisterUser(new Member());

            Assert.Equal(0, session.NumberOfRegistrees);
        }

        [Fact]
        public void RegisterUser_ValidRegistration_RegistersMemberCorrectly()
        {
            Session session = new Session { SessionState = new OpenSessionState(), Capacity = 1 };
            Member member = new Member() { MemberId = 1 };
            session.RegisterUser(member);

            Assert.Equal(1, session.NumberOfRegistrees);
            Assert.Contains(member, session.SessionRegistrees.Select(sr => sr.Member));
        }

        [Fact]
        public void RegisterUser_ChangesAvailableRegistrationSpots()
        {
            Session session = new Session { Capacity = 1, SessionState = new OpenSessionState() };
            session.RegisterUser(new Member());

            Assert.Equal(0, session.AvailableRegistrationSpots);
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
