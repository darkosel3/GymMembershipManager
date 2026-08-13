using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymMembershipManager.Data.Repositories;
using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymMembershipManager.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IMemberRepository _memberRepo;
        private readonly IMembershipRepository _membershipRepo;
        private readonly IGymEquipmentRepository _equipmentRepo;
        private readonly IMembershipTypeRepository _typeRepo;

        public DashboardViewModel(
            IMemberRepository memberRepo,
            IMembershipRepository membershipRepo,
            IGymEquipmentRepository equipmentRepo,
            IMembershipTypeRepository typeRepo)
        {
            _memberRepo = memberRepo;
            _membershipRepo = membershipRepo;
            _equipmentRepo = equipmentRepo;
            _typeRepo = typeRepo;
            Refresh();
        }

        [ObservableProperty] private int _totalMembers;
        [ObservableProperty] private int _totalMemberships;
        [ObservableProperty] private int _activeMemberships;
        [ObservableProperty] private int _expiredMemberships;
        [ObservableProperty] private int _totalEquipment;
        [ObservableProperty] private int _equipmentNeedingMaintenance;
        [ObservableProperty] private decimal _totalRevenue;
        [ObservableProperty] private string _mostPopularType = "-";
        [ObservableProperty] private List<TypeRevenueRow> _revenueByType = new();

        [RelayCommand]
        private void Refresh()
        {
            var members = _memberRepo.GetAll();
            var memberships = _membershipRepo.GetAllWithDetails();
            var equipment = _equipmentRepo.GetAll();
            var types = _typeRepo.GetAll();

            TotalMembers = members.Count;
            TotalMemberships = memberships.Count;
            ActiveMemberships = memberships.Count(m => !m.IsExpired());
            ExpiredMemberships = memberships.Count(m => m.IsExpired());
            TotalEquipment = equipment.Count;
            EquipmentNeedingMaintenance = equipment.Count(e => e.NeedsMaintenance);

            TotalRevenue = memberships
                .Where(m => m.MembershipType != null)
                .Sum(m => m.MembershipType!.Price);

            var grouped = memberships
                .Where(m => m.MembershipType != null)
                .GroupBy(m => m.MembershipType!.Name)
                .Select(g => new TypeRevenueRow
                {
                    TypeName = g.Key,
                    Count = g.Count(),
                    Revenue = g.Sum(m => m.MembershipType!.Price)
                })
                .OrderByDescending(r => r.Count)
                .ToList();

            RevenueByType = grouped;
            MostPopularType = grouped.FirstOrDefault()?.TypeName ?? "-";
        }
    }

    public class TypeRevenueRow
    {
        public string TypeName { get; set; } = "";
        public int Count { get; set; }
        public decimal Revenue { get; set; }
    }
}