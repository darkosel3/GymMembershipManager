using CommunityToolkit.Mvvm.ComponentModel;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymMembershipManager.Models
{
    public partial class DayCell : ObservableObject
    {
        [ObservableProperty] private DateTime date;
        [ObservableProperty] private bool isPaid;

        public int DayNumber => Date.Day;
    }
}