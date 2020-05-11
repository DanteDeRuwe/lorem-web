using System;

namespace G09projectenII.Models.DTOs
{
    public class SessionDTO
    {
        public decimal Id { get; }
        public string Description { get; }
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
        public string Externallink { get; }
        public string Location { get; }
        public string Speakername { get; }
        public string Title { get; }
        public MemberDTO Organiser { get; }
        public AnnouncementDTO MostRecentAnnouncement { get; }
        public int AvailableRegistrationSpots { get; }
        public int NumberOfAttendees { get; }
        public bool HasStarted { get; }
        public bool IsOpen { get; }

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
            var lastAnnouncement = session.MostRecentAnnouncement;
            MostRecentAnnouncement = lastAnnouncement != null ? new AnnouncementDTO(lastAnnouncement) : null;
            AvailableRegistrationSpots = session.AvailableRegistrationSpots;
            NumberOfAttendees = session.NumberOfAttendees;
            HasStarted = session.hasStarted;
            IsOpen = session.IsOpen;
        }
    }
}