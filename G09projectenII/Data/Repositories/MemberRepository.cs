using G09projectenII.Models;
using G09projectenII.Models.Repository_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using G09projectenII.Models.DTOs;

namespace G09projectenII.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Member> _members;

        public MemberRepository(AppDbContext dbContext)
        {
            _context = dbContext;
            _members = dbContext.Members;
        }

        public Member GetBy(int id) => _members.SingleOrDefault(m => m.MemberId == id);
        public Member GetBy(string username) => _members.SingleOrDefault(m => username == m.Username);
        public ICollection<Member> GetAll() => _members.ToList();
        public void Update(Member member) => _context.Update(member);
        public void SaveChanges() => _context.SaveChanges();
    }
}
