using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Models
{
    public class Membership : BaseEntity, IExpirable
    {
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public int MembershipTypeId { get; set; }
        public MembershipType? MembershipType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsExpired() => DateTime.Now > ExpiryDate;
    }
}
