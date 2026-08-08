using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MemberViewModel MemberViewModel { get; }
        public MembershipTypeViewModel MembershipTypeViewModel { get; }
        public GymEquipmentViewModel GymEquipmentViewModel { get; }
        public MembershipViewModel MembershipViewModel { get; }

        public MainViewModel(MemberViewModel memberVm, MembershipTypeViewModel typeVm, GymEquipmentViewModel equipmentVm, MembershipViewModel membershipVm)
        {
            MemberViewModel = memberVm;
            MembershipTypeViewModel = typeVm;
            GymEquipmentViewModel = equipmentVm;
            MembershipViewModel = membershipVm;
        }
    }
}
