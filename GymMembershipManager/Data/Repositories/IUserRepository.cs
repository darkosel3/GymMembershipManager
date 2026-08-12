using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Remove(int id);
        void Update(User user);
        List<User> GetAll();
        User? GetByUsername(string username);
    }
}
