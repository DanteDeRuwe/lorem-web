namespace G09projectenII.Models
{
    public class SessionAttendees
    {
        public decimal Id { get; set; }
        public decimal MemberId { get; set; }

        public Session Session { get; set; }
        public Member Member { get; set; }
    }
}
