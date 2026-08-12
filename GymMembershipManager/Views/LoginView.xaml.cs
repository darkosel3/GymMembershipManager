using GymMembershipManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace GymMembershipManager.Views
{
    public partial class LoginView : Window
    {
        public LoginView(LoginViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            PasswordBox.PasswordChanged += (s, e) =>
            {
                vm.Password = PasswordBox.Password;
            };

            vm.RequestClose += () =>
            {
                DialogResult = vm.IsLoginSuccessful;
                Close();
            };
        }
    }
}