using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    public interface IMemberRepository
    {
        List<Member> GetAll();
        Member GetById(int id);
        void Add(Member member);
        void Update(Member member);
        void Delete(int id);


    }
}
