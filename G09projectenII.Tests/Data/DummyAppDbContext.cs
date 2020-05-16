using G09projectenII.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace G09projectenII.Tests.Data
{
    public class DummyAppDbContext : DbContext
    {

        #region Properties
        public ICollection<Member> Members { get; }
        public Member LoginTest { get; }
        #endregion

        public DummyAppDbContext()
        {
            LoginTest = new Member();
            LoginTest.Username = "test";
            LoginTest.Password = "test";

            Members = new[] { LoginTest };
        }

    }
}
