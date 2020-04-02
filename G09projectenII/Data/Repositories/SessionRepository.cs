using G09projectenII.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace G09projectenII.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Session> _sessions;

        public SessionRepository(AppDbContext dbContext)
        {
            _context = dbContext;
            _sessions = dbContext.Sessions;
        }

        public ICollection<Session> GetAll() => _sessions
            .Include(s => s.SessionRegistrees)
            .Include(s => s.SessionRegistrees)
            .Include(s => s.Announcements)
            .ToList();

        public Session GetBy(int id) => GetAll().SingleOrDefault(s => s.Id == id);
        public void Update(Session session) => _context.Update(session);
        public void SaveChanges() => _context.SaveChanges();
    }
}
