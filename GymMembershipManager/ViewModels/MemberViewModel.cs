using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymMembershipManager.Data.Repositories;
using GymMembershipManager.Models;
using GymMembershipManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Data;


namespace GymMembershipManager.ViewModels
{
    public partial class MemberViewModel : ObservableObject
    {
        private readonly IMemberRepository _repository;
        private readonly ISerializationService _serializationService;
        public IWindowService _windowService;

        [ObservableProperty] private ObservableCollection<MonthGroup> monthsInYear = new();
        [ObservableProperty] private int currentYear = DateTime.Now.Year;
        [ObservableProperty] private bool isDropDownOpen;
        [ObservableProperty] private ObservableCollection<Member> members = new();
        [ObservableProperty] private Member? selectedMember = null;
        [ObservableProperty] private string searchText = string.Empty;
        private ICollectionView _membersView;
        public ICollectionView MembersView => _membersView;

        public MemberViewModel(IMemberRepository repository, IWindowService windowService, ISerializationService serializationService)
        {   
            _repository = repository;
            _windowService = windowService;
            _serializationService = serializationService;

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
            Members.Clear();
            foreach (var member in _repository.GetAllWithMemberships())
                Members.Add(member);
        }

        [RelayCommand]
        private void DeleteMember()
        {
            if (SelectedMember == null) return;
            _repository.Delete(SelectedMember.Id);
            LoadMembers();
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
        }

        [RelayCommand]
        private void OpenEditMemberWindow()
        {
            if (SelectedMember == null)
            {
                MessageBox.Show("Morate selektovati člana za izmenu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _windowService.OpenWindow<Views.AddMemberView>( window =>
            {
                if (window.DataContext is AddMemberViewModel vm)
                    vm.LoadMember(SelectedMember);
            });
            LoadMembers();
        }

        [RelayCommand]
        private void ExportJson()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON fajl (*.json)|*.json",
                FileName = "clanovi_export"
            };
            if (dialog.ShowDialog() != true) return;

            var dtos = MapMembersToDto();
            _serializationService.ExportJson(dtos, dialog.FileName);
            MessageBox.Show("Eksport uspešan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        private void ExportXml()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "XML fajl (*.xml)|*.xml",
                FileName = "clanovi_export"
            };
            if (dialog.ShowDialog() != true) return;

            var dtos = MapMembersToDto();
            _serializationService.ExportXml(dtos, dialog.FileName);
            MessageBox.Show("Eksport uspešan.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        private void ImportJson()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON fajl (*.json)|*.json"
            };
            if (dialog.ShowDialog() != true) return;

            var dtos = _serializationService.ImportJson(dialog.FileName);
            ImportMembers(dtos);
            MessageBox.Show($"Importovano {dtos.Count} članova.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        private void ImportXml()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "XML fajl (*.xml)|*.xml"
            };
            if (dialog.ShowDialog() != true) return;

            var dtos = _serializationService.ImportXml(dialog.FileName);
            ImportMembers(dtos);
            MessageBox.Show($"Importovano {dtos.Count} članova.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<MemberExportDto> MapMembersToDto()
        {
            var members = _repository.GetAllWithMemberships();
            return members.Select(m => new MemberExportDto
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                PhoneNumber = m.PhoneNumber,
                BirthDate = m.BirthDate,
                DateJoined = m.DateJoined,
                Memberships = m.MemberShips.Select(ms => new MembershipExportDto
                {
                    MembershipTypeName = ms.MembershipType?.Name ?? "",
                    StartDate = ms.StartDate,
                    ExpiryDate = ms.ExpiryDate
                }).ToList()
            }).ToList();
        }
        private void ImportMembers(List<MemberExportDto> dtos)
        {
            foreach (var dto in dtos)
            {
                var member = new Member
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PhoneNumber = dto.PhoneNumber,
                    BirthDate = dto.BirthDate,
                    DateJoined = dto.DateJoined
                };
                _repository.Add(member);
            }
            LoadMembers();
        }
    }
}
