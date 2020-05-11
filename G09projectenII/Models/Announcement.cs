using System;

namespace G09projectenII.Models
{
    public class Announcement
    {
        public decimal AnnouncementId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Title { get; set; }
        public decimal SessionId { get; set; }
        public decimal AuthorMemberId { get; set; }
        public string Text { get; set; }

        public Member Author { get; set; }
        public Session Session { get; set; }

        public override string ToString() => string.Join(" | ", AnnouncementId, Timestamp.ToShortDateString(),
                Timestamp.ToShortTimeString(), Title, SessionId, AuthorMemberId, Text);
    }
}
