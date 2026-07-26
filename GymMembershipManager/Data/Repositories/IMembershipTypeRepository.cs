using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    internal interface IMembershipTypeRepository
    {
        void Add(MembershipType type);
        void Remove(int id);
        void Update(MembershipType type);
        List<MembershipType> GetAll();

    }
}
