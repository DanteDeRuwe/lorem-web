using System.Collections.Generic;

namespace G09projectenII.Models
{
    public class SessionCalendar
    {
        public decimal CalendarId { get; set; }
        public string Enddate { get; set; }
        public string Startdate { get; set; }

        public virtual ICollection<Session> Session { get; set; }

        public SessionCalendar() => Session = new HashSet<Session>();

    }
}
