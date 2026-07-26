using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    public interface IMembershipRepository
    {
        List<Membership> GetAll();
        List<Membership> GetAllWithDetails();

        List<Membership> GetByUserId(int id);
        void Add(Membership membership);
        void Delete(int id);
        bool Update(Membership membership);


    }
}
