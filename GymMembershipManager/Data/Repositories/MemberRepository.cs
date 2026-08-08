using GymMembershipManager.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository (AppDbContext contex) => _context = contex;
            
        public List<Member> GetAll()
        {
            return _context.Members.ToList();
        }

        public void Add(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var member = _context.Members.Find(id);

            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }

        }
        public void Update(Member member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
        }

        public Member GetById(int id)
        {
            return _context.Members.Find(id);
        }
    }
}
