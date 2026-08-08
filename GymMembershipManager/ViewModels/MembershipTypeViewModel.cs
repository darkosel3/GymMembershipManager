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
using System.Windows.Data;

namespace GymMembershipManager.ViewModels
{
    public partial class MembershipTypeViewModel : ObservableObject
    {
        private readonly IMembershipTypeRepository _repository;
        public IWindowService _windowService;
        public ICollectionView _membershipTypeView;
        public ICollectionView MembershipTypeView => _membershipTypeView;
        
        [ObservableProperty] private MembershipType? selectedType = null;
        [ObservableProperty] private ObservableCollection<MembershipType> types = new();


        public MembershipTypeViewModel(IMembershipTypeRepository repository, IWindowService windowService)
        {
            _repository = repository;
            _windowService = windowService;
            LoadTypes();
            _membershipTypeView = CollectionViewSource.GetDefaultView(Types);
        }

        [RelayCommand]
        private void LoadTypes()
        {
            Types.Clear();
            foreach (var type in _repository.GetAll())
                Types.Add(type);
        }

        [RelayCommand]
        private void OpenMembershipTypeWindow()
        {
            _windowService.OpenWindow<Views.MembershipTypeView>();
        }

        [RelayCommand]
        private void AddType()
        {
            var newType = new MembershipType
            {
                Name = "Novi tip",
                Price = 0,
                DurationInDays = 30
            };
            _repository.Add(newType);
            LoadTypes();
            SelectedType = Types.FirstOrDefault(t => t.Id == newType.Id);
        }

        [RelayCommand]
        private void SaveType()
        {
            if (SelectedType == null) return;
            _repository.Update(SelectedType);
            LoadTypes();
        }

        [RelayCommand]
        private void DeleteType()
        {
            if (SelectedType == null) return;
            _repository.Remove(SelectedType.Id);
            LoadTypes();
        }

    }
}
