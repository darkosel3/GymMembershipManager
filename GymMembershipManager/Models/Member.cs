using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Models
{
    public class Member : BaseEntity
    {
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime BirthDate { get; set; }

        public List<Membership> MemberShips { get; set; } = new();
    }
}
