using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManager.Services
{
    public interface IWindowService
    {
        void OpenWindow<TWindow>() where TWindow : Window;
    }
}
