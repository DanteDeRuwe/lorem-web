using System.Collections.Generic;

namespace G09projectenII.Models
{
    public interface ISessionCalendarRepository
    {
        SessionCalendar GetBy(int id);
        ICollection<SessionCalendar> GetAll();
        void Update(SessionCalendar calendar);
        void SaveChanges();
    }
}