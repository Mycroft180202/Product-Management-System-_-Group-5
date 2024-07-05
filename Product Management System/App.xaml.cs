using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Product_Management_System.Repositories;
using Product_Management_System.Services;
using Product_Management_System.Data;
using Product_Management_System.Views.Authentication;
using Product_Management_System.Views.ADMIN;

namespace Product_Management_System
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Đăng ký DbContext
            services.AddDbContext<ProductManagementDbContext>();

            // Đăng ký Repositories
            services.AddTransient<IUserRepository, UserRepository>();

            // Đăng ký Services
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            // Đăng ký Windows
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginWindow>();
            services.AddTransient<AdminDashboardWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var loginWindow = serviceProvider.GetService<LoginWindow>();
            loginWindow.Show();
        }
    }
}
