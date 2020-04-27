using System;
using System.Linq;

namespace G09projectenII.Models.DTOs
{
    public class SessionDTO
    {
        public decimal Id { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? Externallink { get; set; }
        public string? Location { get; set; }
        public string? Speakername { get; set; }
        public string? Title { get; set; }
        public MemberDTO Organiser { get; set; }
        public AnnouncementDTO MostRecentAnnouncement { get; set; }
        public int AvailableRegistrationSpots { get; set; }
        public int numberOfAttendances { get; set; }
        public bool hasStarted { get; set; }

        public SessionDTO(Session session)
        {
            Id = session.Id;
            Description = session.Description;
            StartDateTime = session.StartDateTime;
            EndDateTime = session.EndDateTime;
            Location = session.Location;
            Title = session.Title;
            Speakername = session.Speakername;
            Externallink = session.Externallink;
            Organiser = new MemberDTO(session.Member);
            Announcement lastAnnouncement = session.Announcements.OrderBy(a => a.Timestamp).LastOrDefault();
            MostRecentAnnouncement = lastAnnouncement != null ? new AnnouncementDTO(lastAnnouncement) : null;
            AvailableRegistrationSpots = (session.Capacity == null) ? 0 : (int)(session.Capacity - session.SessionRegistrees.Count);
            numberOfAttendances = session.SessionAttendees.Count;
            hasStarted = session.SessionState.ToInt() != 0;
        }
    }
}