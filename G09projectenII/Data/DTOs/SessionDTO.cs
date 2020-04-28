using System;
using System.Linq;

namespace G09projectenII.Models.DTOs
{
    public class SessionDTO
    {
        public decimal Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Externallink { get; set; }
        public string Location { get; set; }
        public string Speakername { get; set; }
        public string Title { get; set; }
        public MemberDTO Organiser { get; set; }
        public AnnouncementDTO MostRecentAnnouncement { get; set; }
        public int AvailableRegistrationSpots { get; set; }
        public int NumberOfAttendees { get; set; }
        public bool HasStarted { get; set; }
        public bool IsOpen { get; set; }

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
            var lastAnnouncement = session.GetMostRecentAnnouncement();
            MostRecentAnnouncement = lastAnnouncement != null ? new AnnouncementDTO(lastAnnouncement) : null;
            AvailableRegistrationSpots = session.GetAvailableRegistrationSpots();
            NumberOfAttendees = session.GetNumberOfAttendees();
            HasStarted = session.hasStarted();
            IsOpen = session.IsOpen();
        }
    }
}