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
    public class MembershipRepository : IMembershipRepository
    {
        private readonly AppDbContext _context;
        public MembershipRepository(AppDbContext context) => _context = context; 
        public void Add(Membership membership)
        {
            _context.Memberships.Add(membership);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var membership = _context.Memberships.Find(id);
            if(membership != null)
            {
                _context.Memberships.Remove(membership);
                _context.SaveChanges();
            }

        }

        public List<Membership> GetAll()
        {
            return _context.Memberships.ToList();
        }

        public List<Membership> GetAllWithDetails()
        {
            return _context.Memberships
                           .Include(membership => membership.Member)
                           .Include(membership => membership.MembershipType)
                           .ToList();
        }

        public List<Membership> GetByUserId(int id)
        {
            return _context.Memberships.Where(m => m.MemberId == id).ToList();
        }

        public bool Update(Membership membership)
        {
            try
            {
                _context.Memberships.Update(membership);
                return _context.SaveChanges() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }

        }
    }
}
