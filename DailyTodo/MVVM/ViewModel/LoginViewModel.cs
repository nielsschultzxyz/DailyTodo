using DailyTodo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DailyTodo.MVVM.Views;
using DailyTodo.MVVM.Model;
using System.Security;
using DailyTodo.Repositories;
using System.Net;
using System.Threading;
using System.Security.Principal;
using System.Windows.Input;

namespace DailyTodo.MVVM.ViewModel
{
    internal class LoginViewModel : ObservableObject
    {
        private IUserRepository _userRepository;
        public static bool _ShowMainWindow;
        public static UserModel _AppUser; 

        private string _username = "";
        public  string Username
        {
            get { return (string)_username; }
            set
            {
                if (Username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private SecureString _password;
        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _errorMessage = "";
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CreateAccountCommand { get; set; }
        public RelayCommand LoginProblemCommand { get; set; }
        public RelayCommand ShutDownCommand { get; set; }
        public RelayCommand MoveWindowCommand { get; set; }

        // ctor LoginViewModel
        public LoginViewModel()
        {
            _userRepository = new UserRepository();

            // Window commands
            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });
            ShutDownCommand = new RelayCommand(o => { Application.Current.Shutdown(); });

            LoginCommand = new RelayCommand(o => {

                var isValidUser = _userRepository.AuthenicateUser(new NetworkCredential(Username, Password));

                // Username & password was found in the database -> Window change
                if (isValidUser)
                {
                    _AppUser = _userRepository.GetByUsername(Username);
                    _AppUser.ValidUser = true;
                    _ShowMainWindow = _AppUser.ValidUser;
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);

                }
                else
                    ErrorMessage = "Invalid username or password";
            });

            CreateAccountCommand = new RelayCommand(o => {
                MessageBox.Show("WIP");
            });

            LoginProblemCommand = new RelayCommand(o => {
                MessageBox.Show("Locks like a problem :x");
            });
        }

        public void CallKeyDown(KeyEventArgs e)
        {

        }
    }
}
