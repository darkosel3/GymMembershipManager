using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Models
{
    internal interface IExpirable
    {
        DateTime ExpiryDate { get; }
        bool IsExpired();
    }
}
