using System.Collections.Generic;

namespace G09projectenII.Models
{
    public class SessionCalendar
    {
        public decimal CalendarId { get; set; }
        public string Enddate { get; set; }
        public string Startdate { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        public SessionCalendar() => Sessions = new HashSet<Session>();

    }
}
