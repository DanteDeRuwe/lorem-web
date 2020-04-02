using System.Collections.Generic;

namespace G09projectenII.Models
{
    public interface ISessionRepository
    {
        Session GetBy(int id);
        ICollection<Session> GetAll();
        ICollection<Session> GetBy(SessionCalendar calendar);
        void Update(Session session);
        void SaveChanges();
    }
}
