using System;
using System.Collections.Generic;

namespace G09projectenII.Models
{
    public class Session
    {
        public decimal Id { get; set; }
        public int? Capacity { get; set; }
        public string Description { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
        public string Externallink { get; set; }
        public string Location { get; set; }
        public string Speakername { get; set; }
        public string Title { get; set; }
        public decimal CalendarId { get; set; }
        public decimal MemberId { get; set; }
        public string Type { get; set; }

        public SessionCalendar Calendar { get; set; }
        public Member Member { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
        public ICollection<SessionAttendees> SessionAttendees { get; set; }
        public ICollection<SessionRegistrees> SessionRegistrees { get; set; }

        public SessionState SessionState { get; set; }

        public Session()
        {
            Announcements = new HashSet<Announcement>();
            SessionAttendees = new HashSet<SessionAttendees>();
            SessionRegistrees = new HashSet<SessionRegistrees>();
        }

        public override string ToString() => string.Join(" | ", Id, Title, Location, Speakername, Type, Starttime.ToShortDateString(), Starttime.ToShortTimeString()
            , Endtime.ToShortDateString(), Endtime.ToShortTimeString());
    }
}
