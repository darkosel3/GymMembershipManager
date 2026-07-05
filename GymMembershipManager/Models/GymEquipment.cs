using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Models
{
    public class GymEquipment : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // npr. "Klupa za bench press"
        public string Category { get; set; } = string.Empty; // "Kardio", "Tegovi", "Sprave"
        public DateTime PurchaseDate { get; set; }
        public bool NeedsMaintenance { get; set; }
    }
}
