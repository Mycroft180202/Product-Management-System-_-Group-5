using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Product_Management_System;
using ProductManagementSystem.Services.Authentication;

namespace ProductManagementSystem.ViewModels.Authentication
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private readonly IAuthenticationService _authenticationService;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public Action CloseAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            _authenticationService = new AuthenticationService(); 
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            var isAuthenticated = _authenticationService.Authenticate(Username, Password);
            if (isAuthenticated)
            {
                MessageBox.Show("Login successful!");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                CloseAction?.Invoke();
            }   
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
