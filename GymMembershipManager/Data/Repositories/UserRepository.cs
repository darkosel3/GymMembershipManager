using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly AppDbContext _context;

        public UserRepository(AppDbContext contex) => _context = contex;

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Remove(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

        }
        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
