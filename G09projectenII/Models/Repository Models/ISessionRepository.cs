using System.Collections.Generic;

namespace G09projectenII.Models
{
    public interface ISessionRepository
    {
        Session GetBy(int id);
        ICollection<Session> GetAll();
        void Update(Session session);
        void SaveChanges();
    }
}
