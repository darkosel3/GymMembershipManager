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
            ShutdownMode = ShutdownMode.OnExplicitShutdown;


            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var loginWindow = ServiceProvider.GetRequiredService<Views.LoginView>();
            if (loginWindow.ShowDialog() == true)
            {
                ShutdownMode = ShutdownMode.OnMainWindowClose;
                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();
            services.AddScoped<IGymEquipmentRepository, GymEquipmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IPdfReportService, PdfReportService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MembershipViewModel>();
            services.AddTransient<GymEquipmentViewModel>();
            services.AddTransient<MemberViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<AddMemberViewModel>();
            services.AddTransient<ViewModels.MembershipTypeViewModel>();
            services.AddTransient<Views.LoginView>();
            services.AddTransient<Views.MembershipTypeView>();
            services.AddTransient<MainWindow>();
            services.AddTransient<Views.AddMemberView>();
            services.AddSingleton<IMembershipFactory, MembershipFactory>();

        }

    }

}
