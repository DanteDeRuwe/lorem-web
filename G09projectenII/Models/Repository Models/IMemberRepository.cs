using System.Collections.Generic;

namespace G09projectenII.Models.Repository_Models
{
    public interface IMemberRepository
    {
        Member GetBy(int id);
        public Member GetBy(string username);
        ICollection<Member> GetAll();
        void Update(Member member);
        void SaveChanges();
    }
}
