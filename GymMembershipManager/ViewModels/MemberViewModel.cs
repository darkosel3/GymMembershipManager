using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymMembershipManager.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymMembershipManager.Models;
using GymMembershipManager.Services;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Automation.Peers;
using System.ComponentModel;

namespace GymMembershipManager.ViewModels
{
    public partial class MemberViewModel : ObservableObject
    {
        private readonly IMemberRepository _repository;
        public IWindowService _windowService;


        [ObservableProperty] private ObservableCollection<MonthGroup> monthsInYear = new();
        [ObservableProperty] private int currentYear = DateTime.Now.Year;
        [ObservableProperty] private bool isDropDownOpen;
        [ObservableProperty] private ObservableCollection<Member> members = new();
        [ObservableProperty] private Member? selectedMember = null;
        [ObservableProperty] private string searchText = string.Empty;
        private ICollectionView _membersView;
        public ICollectionView MembersView => _membersView;





        public MemberViewModel(IMemberRepository repository, IWindowService windowService)
        {   
            
            _repository = repository;
            _windowService = windowService;
            LoadMembers();
            _membersView = CollectionViewSource.GetDefaultView(Members);
            _membersView.Filter = FilterMembers;
            if (Members.Count > 0)
            {
                SelectedMember = Members[0];
            }
        }

        private bool FilterMembers(object obj)
        {
            if (obj is not Member m)
                return false;

            if (string.IsNullOrWhiteSpace(SearchText))
                return true;
            
            return m.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
        }

        [RelayCommand]
        private void LoadMembers()
        {
            Members = new ObservableCollection<Member>(_repository.GetAll());
        }

        partial void OnSearchTextChanged(string value)
        {
            _membersView.Refresh();
        }

        private void GenerateDaysForSelectedMember()
        {
            MonthsInYear.Clear();

            if (SelectedMember == null) return;

            var culture = System.Globalization.CultureInfo.CurrentCulture;

            for (int month = 1; month <= 12; month++)
            {
                var group = new MonthGroup
                {
                    MonthName = culture.DateTimeFormat.GetMonthName(month)
                };

                int daysCount = DateTime.DaysInMonth(CurrentYear, month);

                for (int day = 1; day <= daysCount; day++)
                {
                    var date = new DateTime(CurrentYear, month, day);

                    bool isPaid = SelectedMember.MemberShips.Any(m =>
                        date >= m.StartDate.Date && date <= m.ExpiryDate.Date);

                    group.Days.Add(new DayCell { Date = date, IsPaid = isPaid });
                }

                MonthsInYear.Add(group);
            }
        }

        partial void OnSelectedMemberChanged(Member? value)
        {
            GenerateDaysForSelectedMember();
        }

        partial void OnSearchTextChanged(string? oldValue, string newValue)
        {
            _membersView.Refresh();
            IsDropDownOpen = !string.IsNullOrEmpty(newValue);
        }


        partial void OnCurrentYearChanged(int value)
        {
            GenerateDaysForSelectedMember();
        }


        [RelayCommand]
        private void PreviousYear()
        {
            CurrentYear--;
        }

        [RelayCommand]
        private void NextYear()
        {
            CurrentYear++;
        }

        [RelayCommand]
        private void OpenAddMemberWindow()
        {
            _windowService.OpenWindow<Views.AddMemberView>();
            LoadMembers();
            //var newMember = new Member
            //{
            //    FirstName = "Test",
            //    LastName = "testic",
            //    PhoneNumber = "1231231",
            //    DateJoined = DateTime.Now
            //};
            //_repository.Add(newMember);
            //Members.Add(newMember);
        }
    }
}
