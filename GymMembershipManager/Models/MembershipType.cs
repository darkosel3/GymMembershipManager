
using System.Collections.Generic;
namespace GymMembershipManager.Models
{
    public class MembershipType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }

        public List<Membership> MemberShips { get; set; } = new();
    }
}
