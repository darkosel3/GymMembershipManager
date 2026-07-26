using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Data.Repositories
{
    public class GymEquipmentRepository : IGymEquipmentRepository
    {
        private readonly AppDbContext _context;

        public GymEquipmentRepository(AppDbContext context) => _context = context;

        public List<GymEquipment> GetAll()
        {
            return _context.GymEquipments.ToList();
        }

        public void Add(GymEquipment equipment)
        {
            _context.GymEquipments.Add(equipment);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            GymEquipment? equipment = _context.GymEquipments.Find(id);

            if (equipment != null)
            {
                _context.GymEquipments.Remove(equipment);
                _context.SaveChanges();
            }
        }

        public void Update(GymEquipment equipment)
        {
            _context.GymEquipments.Update(equipment);
            _context.SaveChanges();
        }
    }
}
