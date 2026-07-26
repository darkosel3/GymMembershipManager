using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly AppDbContext _context;

        public MembershipTypeRepository(AppDbContext context) => _context = context;


        public void Add(MembershipType type)
        {
            _context.MembershipTypes.Add(type);
            _context.SaveChanges();
        }

        public List<MembershipType> GetAll()
        {
           return _context.MembershipTypes.ToList();
        }

        public void Remove(int id)
        {
            MembershipType mt = _context.MembershipTypes.Find(id);

            if (mt != null)
            {
                _context.MembershipTypes.Remove(mt);
                _context.SaveChanges();
            }
        }

        public void Update(MembershipType type)
        {
            _context.MembershipTypes.Update(type);
            _context.SaveChanges();
        }
    }
}
