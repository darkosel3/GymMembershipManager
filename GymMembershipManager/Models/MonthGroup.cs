using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GymMembershipManager.Models
{
    public partial class MonthGroup : ObservableObject
    {
        [ObservableProperty] private string monthName = string.Empty;
        public ObservableCollection<DayCell> Days { get; set; } = new();
    }
}