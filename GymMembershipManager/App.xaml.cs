using GymMembershipManager.Data;
using GymMembershipManager.Data.Repositories;
using GymMembershipManager.Services;
using GymMembershipManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
namespace GymMembershipManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();
            services.AddScoped<IGymEquipmentRepository, GymEquipmentRepository>();

            services.AddSingleton<IWindowService, WindowService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MembershipViewModel>();
            services.AddTransient<GymEquipmentViewModel>();
            services.AddTransient<MemberViewModel>();
            services.AddTransient<AddMemberViewModel>();
            services.AddTransient<ViewModels.MembershipTypeViewModel>();
            services.AddTransient<Views.MembershipTypeView>();
            services.AddTransient<MainWindow>();
            services.AddTransient<Views.AddMemberView>();
        }

    }

}
