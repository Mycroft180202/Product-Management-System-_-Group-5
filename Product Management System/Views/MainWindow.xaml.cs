using Product_Management_System.Data;
using Product_Management_System.Models;
using Product_Management_System.Repositories;
using Product_Management_System.Repositories.Authentication;
using Product_Management_System.Views.Admin;
using Product_Management_System.Views.Authentication;
using Product_Management_System.Views.CostHistory;
using Product_Management_System.Views.ManageProduct;
using System.Windows;
using System.Windows.Input;

namespace Product_Management_System
{
    public partial class MainWindow : Window
    {
        private ProductManagementDbContext dbContext;
        private User currentUser;
        private readonly IUserRepository userRepository;

        public MainWindow(User user)
        {
            InitializeComponent();
            dbContext = new ProductManagementDbContext();
            userRepository = new UserRepository(dbContext);
            currentUser = user;
            if (currentUser.RoleId == 1) // Admin
            {
                btnDashboard.Visibility = Visibility.Visible;
            }
            LoadUserInfo();

        }

        public MainWindow()
        {
            InitializeComponent();
            dbContext = new ProductManagementDbContext();
            userRepository = new UserRepository(dbContext);
            currentUser = userRepository.GetUserByUsername("default_username"); 
            LoadUserInfo();
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboardWindow adminDashboard = new AdminDashboardWindow();
            adminDashboard.Show();
        }

        private void LoadUserInfo()
        {
            if (currentUser != null && !string.IsNullOrEmpty(currentUser.Fullname))
            {
                txtUserFullName.Text = currentUser.Fullname;
            }
            else
            {
                txtUserFullName.Text = "Unknown User";
            }
        }

        private void btnProductInventory_Click(object sender, RoutedEventArgs e)
        {
            frMainContent.Navigate(new ProductInventoryPage());
            ProductInventoryPage productInventoryPage = new ProductInventoryPage(currentUser);
            frMainContent.Navigate(productInventoryPage);
        }

        private void btnPriceHistory_Click(object sender, RoutedEventArgs e)
        {
            ProductPriceHistoryPage priceHistoryPage = new ProductPriceHistoryPage(currentUser);
            frMainContent.Navigate(priceHistoryPage);
        }

        private void txtUserFullName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (currentUser != null)
            {
                UserInfoWindow userInfoWindow = new UserInfoWindow(currentUser, userRepository);
                userInfoWindow.Owner = this;
                bool? result = userInfoWindow.ShowDialog();

                if (result == true)
                {
                    txtUserFullName.Text = currentUser.Fullname;
                }
            }
        }

        private void btnProductManage_Click(object sender, RoutedEventArgs e)
        {
            ManageProductPage priceHistoryPage = new ManageProductPage(currentUser);
            frMainContent.Navigate(priceHistoryPage);
        }

        private void btnCostHistory_Click(object sender, RoutedEventArgs e)
        {
            ProductCostHistoryPage costHistoryPage = new ProductCostHistoryPage(currentUser);
            frMainContent.Navigate(costHistoryPage);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}