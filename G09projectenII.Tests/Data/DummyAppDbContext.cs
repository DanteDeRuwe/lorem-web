using G09projectenII.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BCr = BCrypt.Net.BCrypt;

namespace G09projectenII.Tests.Data
{
    public class DummyAppDbContext : DbContext
    {

        #region Properties
        public ICollection<Member> Members { get; }
        public ICollection<Session> Sessions { get; }
        public Member LoginTest { get; }
        public Session SessionTest { get; }
        #endregion

        public DummyAppDbContext()
        {
            LoginTest = new Member()
            {
                Firstname = "Jan",
                Lastname = "Janssen",
                Username = "test",
                Password = BCr.HashPassword("test", 12),
                Membertype = 0,
                Memberstatus = 0
            };

            SessionTest = new Session()
            { 
                Title = "test session",
                Id = 1,
                Member = LoginTest,
                SessionState = new CreatedSessionState()
            };

            Members = new[] { LoginTest };
            Sessions = new List<Session>
            {
                SessionTest
            };
        }

    }
}
