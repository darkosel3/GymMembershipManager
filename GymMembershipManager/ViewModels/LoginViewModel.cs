using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymMembershipManager.Data.Repositories;
using GymMembershipManager.Services;
using System.Security.Cryptography;
using System.Text;

namespace GymMembershipManager.ViewModels
{
   
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;
        private readonly UserSession _session;

        [ObservableProperty] private string username = string.Empty;
        [ObservableProperty] private string errorMessage = string.Empty;

        public string Password { private get; set; } = string.Empty;
        public bool IsLoginSuccessful { get; private set; }
        public event Action? RequestClose;

        public LoginViewModel(IUserRepository userRepository, UserSession session)
        {
            _userRepository = userRepository;
            _session = session;
        }

        [RelayCommand]
        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Unesite korisničko ime i lozinku.";
                return;
            }

            var user = _userRepository.GetByUsername(Username);

            if (user == null || user.PasswordHash != HashPassword(Password))
            {
                ErrorMessage = "Pogrešno korisničko ime ili lozinka.";
                return;
            }

            _session.Set(user.Username, user.Role);
            IsLoginSuccessful = true;
            RequestClose?.Invoke();
        }
        private static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
