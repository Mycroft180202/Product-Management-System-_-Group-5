using System.Windows;
using Product_Management_System.Models;
using Product_Management_System.Services;
using Product_Management_System.Repositories;
using Product_Management_System.Data;
using Product_Management_System.Views.ADMIN;

namespace Product_Management_System.Views.Authentication
{
    public partial class LoginWindow : Window
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginWindow()
        {
            InitializeComponent();
            var context = new ProductManagementDbContext(); 
            var userRepository = new UserRepository(context);
            _authenticationService = new AuthenticationService(userRepository);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Password;

            var user = _authenticationService.Authenticate(username, password);

            if (user != null)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                if (user.RoleId == 1) // Admin
                {
                    AdminDashboardWindow adminDashboard = new AdminDashboardWindow();
                    adminDashboard.Show();
                }
                else if (user.RoleId == 2) // Staff
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng hoặc tài khoản chưa được kích hoạt.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
