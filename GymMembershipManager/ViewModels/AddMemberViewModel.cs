using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymMembershipManager.Data.Repositories;
using GymMembershipManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymMembershipManager.ViewModels
{
    public partial class AddMemberViewModel : ObservableObject
    {
        private readonly IMemberRepository _repository;

        public event Action? RequestClose;

        //Generator automatski pretvori firstName u FirstName
        [ObservableProperty] private string firstName = string.Empty;
        [ObservableProperty] private string lastName = string.Empty;
        [ObservableProperty] private string phoneNumber = string.Empty;
        [ObservableProperty] private DateTime birthDate = DateTime.Now.AddYears(-18);

        public AddMemberViewModel(IMemberRepository repository){
            _repository = repository;
        }


        [RelayCommand]
        private void Save()
        {
            var member = new Member
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                DateJoined = DateTime.Now,
                BirthDate = BirthDate
            };
            _repository.Add(member);
            RequestClose?.Invoke();
        } 


    }
}
