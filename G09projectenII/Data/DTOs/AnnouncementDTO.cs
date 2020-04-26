using System;

namespace G09projectenII.Models.DTOs
{
    public class AnnouncementDTO
    {
        private DateTime Timestamp { get; set; }
        private string Title { get; set; }
        private string Text { get; set; }
        private string AuthorName { get; set; }

        public AnnouncementDTO(Announcement announcement)
        {
            Timestamp = announcement.Timestamp;
            Title = announcement.Title;
            Text = announcement.Text;
            AuthorName = $"{announcement.Author.Firstname} {announcement.Author.Lastname}";
        }
    }
}