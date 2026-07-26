using GymMembershipManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GymMembershipManager.Views
{
    /// <summary>
    /// Interaction logic for AddMemberView.xaml
    /// </summary>
    public partial class AddMemberView : Window
    {
        public AddMemberView(AddMemberViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.RequestClose += () => { DialogResult = true; Close(); };
        }

    
    }
}
