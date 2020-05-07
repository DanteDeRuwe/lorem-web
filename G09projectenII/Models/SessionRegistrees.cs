namespace G09projectenII.Models
{
    public class SessionRegistrees
    {
        public decimal Id { get; set; }
        public decimal MemberId { get; set; }

        public Session Session { get; set; }
        public Member Member { get; set; }

        public SessionRegistrees(Session session, Member member)
        {
            this.Session = session;
            this.Member = member;
            this.Id = session.Id;
            this.MemberId = member.MemberId;
        }
    }
}
