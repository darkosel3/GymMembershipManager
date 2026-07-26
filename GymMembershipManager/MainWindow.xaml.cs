using GymMembershipManager.Data;
using GymMembershipManager.Data.Repositories;
using GymMembershipManager.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GymMembershipManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MemberViewModel viewModel)
        {
            InitializeComponent();
            var context = new AppDbContext();
            var repository = new MemberRepository(context);
            DataContext = viewModel;
            MemberSearchBox.AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(FixCaretPosition));

        }
        private void FixCaretPosition(object sender, TextChangedEventArgs e)
        {
            var textBox = MemberSearchBox.Template.FindName("PART_EditableTextBox", MemberSearchBox) as TextBox;
            if (textBox != null)
            {
                textBox.CaretIndex = textBox.Text.Length;
                textBox.SelectionLength = 0;
            }
        }


    }
}