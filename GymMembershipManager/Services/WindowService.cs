using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GymMembershipManager.Services
{
    public class WindowService : IWindowService 
    { 
    private readonly IServiceProvider _serviceProvider;

    public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    
    public void OpenWindow<TWindow>() where TWindow : Window
        {
            var window = _serviceProvider.GetRequiredService<TWindow>();
            window.ShowDialog();
        }

    }

}
