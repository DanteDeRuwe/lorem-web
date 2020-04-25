using System.Collections.Generic;

namespace G09projectenII.Models.Repository_Models
{
    public interface IMemberRepository
    {
        Member GetBy(int id);
        ICollection<Member> GetAll();
        void Update(Member member);
        void SaveChanges();
    }
}
