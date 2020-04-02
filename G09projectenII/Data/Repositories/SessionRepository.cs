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

        public Session GetBy(int id) => _sessions.SingleOrDefault(s => s.Id == id);
        public ICollection<Session> GetAll() => _sessions.ToList();
        public void Update(Session session) => _context.Update(session);
        public void SaveChanges() => _context.SaveChanges();
    }
}
