using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace G09projectenII.Models
{
    public class SessionRegistrees
    {
        public decimal Id { get; set; }
        public decimal MemberId { get; set; }

        public Session Session { get; set; }
        public Member Member { get; set; }

        public bool IsAlsoAttendee => Session.SessionAttendees.Any(sa => sa.MemberId == MemberId);

        public SessionRegistrees() { }

        public SessionRegistrees(Session session, Member member)
        {
            Session = session;
            Member = member;
            Id = session.Id;
            MemberId = member.MemberId;
        }
    }
}
