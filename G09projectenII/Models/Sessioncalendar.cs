using System.Collections.Generic;
using System.Linq;

namespace G09projectenII.Models
{
    public class SessionCalendar
    {
        public decimal CalendarId { get; set; }
        public string Enddate { get; set; }
        public string Startdate { get; set; }

        public ICollection<Session> Sessions { get; set; }

        public SessionCalendar() => Sessions = new HashSet<Session>();

        public Session GetSessionBy(int Id) => Sessions.FirstOrDefault(s => s.Id == Id);

        public override string ToString() => string.Join(" | ", Startdate, Enddate);
    }
}