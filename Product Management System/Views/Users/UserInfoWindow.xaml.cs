using Product_Management_System.Data;
using Product_Management_System.Models;
using Product_Management_System.Repositories;
using System.Windows;

namespace Product_Management_System
{
    public partial class UserInfoWindow : Window
    {
        private User currentUser;
        private readonly IUserRepository userRepository;
        private readonly ProductManagementDbContext dbContext;

        public UserInfoWindow(User user, IUserRepository repository)
        {
            InitializeComponent();
            currentUser = user;
            userRepository = repository;
            dbContext = new ProductManagementDbContext();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            txtUsername.Text = currentUser.Username;

            if (dbContext != null)
            {
                var roleName = (from user in dbContext.Users
                                join role in dbContext.Roles on user.RoleId equals role.Id
                                where user.Id == currentUser.Id
                                select role.Name).FirstOrDefault(); 

                txtRole.Text = roleName; 
            }

            txtFullName.Text = currentUser.Fullname;
            txtEmail.Text = currentUser.Email;
            txtPassword.Password = new string('*', currentUser.Password.Length);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentUser.Fullname = txtFullName.Text;
            currentUser.Email = txtEmail.Text;

            if (txtPassword.Password != new string('*', currentUser.Password.Length))
            {
                currentUser.Password = txtPassword.Password;
            }

            userRepository.UpdateUser(currentUser);
            MessageBox.Show("User information updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}