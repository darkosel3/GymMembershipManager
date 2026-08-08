using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymMembershipManager.Data.Repositories;
using GymMembershipManager.Models;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace GymMembershipManager.ViewModels
{
    public partial class MembershipViewModel : ObservableObject
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        [ObservableProperty] private ObservableCollection<Membership> memberships = new();
        [ObservableProperty] private ObservableCollection<Member> allMembers = new();
        [ObservableProperty] private ObservableCollection<MembershipType> allTypes = new();

        [ObservableProperty] private Membership? selectedMembership;
        [ObservableProperty] private Member? newMember;
        [ObservableProperty] private MembershipType? newType;
        [ObservableProperty] private DateTime? newStartDate = DateTime.Today;

        public MembershipViewModel(
            IMembershipRepository membershipRepository,
            IMemberRepository memberRepository,
            IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipRepository = membershipRepository;
            _memberRepository = memberRepository;
            _membershipTypeRepository = membershipTypeRepository;
            LoadAll();
        }

        private void LoadAll()
        {
            LoadMemberships();
            LoadMembers();
            LoadTypes();
        }

        private void LoadMemberships()
        {
            Memberships.Clear();
            foreach (var m in _membershipRepository.GetAll())
                Memberships.Add(m);
        }

        private void LoadMembers()
        {
            AllMembers.Clear();
            foreach (var m in _memberRepository.GetAll())
                AllMembers.Add(m);
        }

        private void LoadTypes()
        {
            AllTypes.Clear();
            foreach (var t in _membershipTypeRepository.GetAll())
                AllTypes.Add(t);
        }

        [RelayCommand]
        private void AddMembership()
        {
            if (NewMember == null || NewType == null || NewStartDate == null)
                return;

            var membership = new Membership
            {
                MemberId = NewMember.Id,
                MembershipTypeId = NewType.Id,
                StartDate = NewStartDate.Value,
                ExpiryDate = NewStartDate.Value.AddDays(NewType.DurationInDays)
            };

            _membershipRepository.Add(membership);
            LoadMemberships();

            // Reset forme
            NewMember = null;
            NewType = null;
            NewStartDate = DateTime.Today;
        }

        [RelayCommand]
        private void DeleteMembership()
        {
            if (SelectedMembership == null) return;
            _membershipRepository.Delete(SelectedMembership.Id);
            LoadMemberships();
        }
    }
}