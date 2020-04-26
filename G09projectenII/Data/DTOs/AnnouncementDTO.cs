using System;

namespace G09projectenII.Models.DTOs
{
    public class AnnouncementDTO
    {
        public DateTime Timestamp { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public MemberDTO Author { get; set; }

        public AnnouncementDTO(Announcement announcement)
        {
            Timestamp = announcement.Timestamp;
            Title = announcement.Title;
            Text = announcement.Text;
            Author = new MemberDTO(announcement.Author);
        }
    }
}