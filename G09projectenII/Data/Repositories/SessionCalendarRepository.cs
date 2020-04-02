using G09projectenII.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace G09projectenII.Data.Repositories
{
    public class SessionCalendarRepository : ISessionCalendarRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<SessionCalendar> _sessioncalendars;

        public SessionCalendarRepository(AppDbContext dbContext)
        {
            _context = dbContext;
            _sessioncalendars = dbContext.SessionCalendars;
        }

        public ICollection<SessionCalendar> GetAll() => _sessioncalendars.ToList();

        public SessionCalendar GetBy(int id) => GetAll().SingleOrDefault(s => s.CalendarId == id);
        public void Update(SessionCalendar calendar) => _context.Update(calendar);
        public void SaveChanges() => _context.SaveChanges();
    }
}

