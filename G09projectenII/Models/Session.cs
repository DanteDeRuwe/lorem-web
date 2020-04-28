using System;
using System.Collections.Generic;
using System.Linq;

namespace G09projectenII.Models
{
    public class Session
    {

        //Properties
        public decimal Id { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Externallink { get; set; }
        public string Location { get; set; }
        public string Speakername { get; set; }
        public string Title { get; set; }
        public decimal CalendarId { get; set; }
        public decimal MemberId { get; set; }
        public string Type { get; set; }

        // Nav Properties
        public SessionCalendar Calendar { get; set; }
        public Member Member { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
        public ICollection<SessionAttendees> SessionAttendees { get; set; }
        public ICollection<SessionRegistrees> SessionRegistrees { get; set; }
        public SessionState SessionState { get; set; }
        public int NumberOfRegistrees => SessionRegistrees.Count();

        public Session()
        {
            Announcements = new HashSet<Announcement>();
            SessionAttendees = new HashSet<SessionAttendees>();
            SessionRegistrees = new HashSet<SessionRegistrees>();
        }

        public override string ToString() => string.Join(" | ", Id, Title, Location, Speakername, Type, StartDateTime.ToShortDateString(), StartDateTime.ToShortTimeString()
            , EndDateTime.ToShortDateString(), EndDateTime.ToShortTimeString());


        public void NextState()
        {
            int nextStateIndex = SessionState.ToInt() + 1;
            SessionState = SessionState.FromInt(nextStateIndex <= 3 ? nextStateIndex : 3);
        }

        public int GetAvailableRegistrationSpots() => (this.Capacity - this.SessionRegistrees.Count);

        public Announcement GetMostRecentAnnouncement() => this.Announcements.OrderBy(a => a.Timestamp).LastOrDefault();

        public int GetNumberOfAttendees() => this.SessionAttendees.Count;
        
        public bool hasStarted() => this.SessionState.ToInt() > 1;
        
        public bool IsOpen() => this.SessionState.ToInt() == 1;
    }
}
