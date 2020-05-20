using System.Collections.Generic;

namespace G09projectenII.Models
{
    public class Member
    {
        public decimal MemberId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Memberstatus { get; set; }
        public int Membertype { get; set; }
        public string Profilepicpath { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string FullName => $"{Firstname} {Lastname}";

        public ICollection<Announcement> Announcements { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<SessionAttendees> SessionAttendees { get; set; }
        public ICollection<SessionRegistrees> SessionRegistrees { get; set; }

        public Member()
        {
            Announcements = new HashSet<Announcement>();
            Sessions = new HashSet<Session>();
            SessionAttendees = new HashSet<SessionAttendees>();
            SessionRegistrees = new HashSet<SessionRegistrees>();
        }

        public override string ToString() => string.Join(" | ", MemberId, Username, Firstname, Lastname, Memberstatus, Membertype);
    }
}
