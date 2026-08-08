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
        private int? _editingMemberId;
        public AddMemberViewModel(IMemberRepository repository){
            _repository = repository;
        }


        public void LoadMember(Member member)
        {
            _editingMemberId = member.Id;
            FirstName = member.FirstName;
            LastName = member.LastName;
            PhoneNumber = member.PhoneNumber;
            BirthDate = member.BirthDate;
        }


        [RelayCommand]
        private void Save()
        {
            if (_editingMemberId.HasValue)
            {
                var existing = _repository.GetById(_editingMemberId.Value);
                if (existing != null)
                {
                    existing.FirstName = FirstName;
                    existing.LastName = LastName;
                    existing.PhoneNumber = PhoneNumber;
                    existing.BirthDate = BirthDate;
                    _repository.Update(existing);
                }
            }
            else
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
            }
            RequestClose?.Invoke();
        } 


    }
}
