using CommunityToolkit.Mvvm.ComponentModel;
using GymMembershipManager.Services;
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
        public DashboardViewModel DashboardViewModel { get; }

        public UserSession Session { get; }
        public bool IsManager => Session.IsManager;
        public string WelcomeText => $"Ulogovan: {Session.Username} ({Session.Role})";
        public MainViewModel(MemberViewModel memberVm, MembershipTypeViewModel typeVm, GymEquipmentViewModel equipmentVm, MembershipViewModel membershipVm, DashboardViewModel dashboardVm, UserSession session)
        {
            MemberViewModel = memberVm;
            MembershipTypeViewModel = typeVm;
            GymEquipmentViewModel = equipmentVm;
            MembershipViewModel = membershipVm;
            DashboardViewModel = dashboardVm;
            Session = session;
        }
    }
}
