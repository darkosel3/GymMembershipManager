using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    public interface IGymEquipmentRepository
    {
        List<GymEquipment> GetAll();
        void Add(GymEquipment equipment);
        void Remove(int id);
        void Update(GymEquipment equipment);
    }
}