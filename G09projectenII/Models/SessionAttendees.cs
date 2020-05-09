namespace G09projectenII.Models
{
    public class SessionAttendees
    {
        public decimal Id { get; set; }
        public decimal MemberId { get; set; }

        public Session Session { get; set; }
        public Member Member { get; set; }

        public SessionAttendees() { }

        public SessionAttendees(Session session, Member member)
        {
            Session = session;
            Member = member;
            Id = session.Id;
            MemberId = member.MemberId;
        }
    }
}
