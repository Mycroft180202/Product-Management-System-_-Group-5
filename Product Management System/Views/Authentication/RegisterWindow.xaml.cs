using System;
using System.Windows;
using Product_Management_System.Data;
using Product_Management_System.Models;
using Product_Management_System.Repositories.Authentication;

namespace Product_Management_System.Views.Authentication
{
    public partial class RegisterWindow : Window
    {
        private readonly UserRepository _userRepository;

        public RegisterWindow()
        {
            InitializeComponent();
            _userRepository = new UserRepository(new ProductManagementDbContext());
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string username = txtUser.Text;
            string password = txtPass.Password;
            string confirmPassword = txtConfirmPass.Password;

            // Validate that all fields are filled
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate email format
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Invalid email format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate password length and strength (example: at least 8 characters)
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
            {
                MessageBox.Show("Password must contain at least one uppercase letter, one lowercase letter, and one digit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate that passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate that username does not already exist
            if (_userRepository.GetUserByUsername(username) != null)
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create new user and register
            User newUser = new User
            {
                Username = username,
                Password = password,
                Fullname = fullName,
                Email = email,
                IsActive = true,
                RoleId = 2
            };

            try
            {
                _userRepository.AddUser(newUser);
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
