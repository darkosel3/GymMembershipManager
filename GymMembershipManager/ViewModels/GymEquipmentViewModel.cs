using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymMembershipManager.Data.Repositories;
using GymMembershipManager.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace GymMembershipManager.ViewModels
{
    public partial class GymEquipmentViewModel : ObservableObject
    {
        private readonly IGymEquipmentRepository _repository;

        [ObservableProperty] private ObservableCollection<GymEquipment> equipment = new();
        [ObservableProperty] private GymEquipment? selectedEquipment = null;

        public GymEquipmentViewModel(IGymEquipmentRepository repository)
        {
            _repository = repository;
            LoadEquipment();
        }

        [RelayCommand]
        private void LoadEquipment()
        {
            Equipment.Clear();
            foreach (var item in _repository.GetAll())
                Equipment.Add(item);
        }

        [RelayCommand]
        private void AddEquipment()
        {
            var newEquipment = new GymEquipment
            {
                Name = "Nova oprema",
                Category = "Sprave",
                PurchaseDate = DateTime.Now,
                NeedsMaintenance = false
            };
            _repository.Add(newEquipment);
            LoadEquipment();
            SelectedEquipment = Equipment.FirstOrDefault(e => e.Id == newEquipment.Id);
        }

        [RelayCommand]
        private void SaveEquipment()
        {
            if (SelectedEquipment == null) return;
            _repository.Update(SelectedEquipment);
            LoadEquipment();
        }

        [RelayCommand]
        private void DeleteEquipment()
        {
            if (SelectedEquipment == null) return;
            _repository.Remove(SelectedEquipment.Id);
            LoadEquipment();
        }
    }
}