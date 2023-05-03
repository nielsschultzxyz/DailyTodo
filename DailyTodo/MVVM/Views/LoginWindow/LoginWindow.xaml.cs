using DailyTodo.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DailyTodo.MVVM.Views
{
    /// <summary>
    /// Interaktionslogik für LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(LoginViewModel._ShowMainWindow)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

            }
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    MessageBox.Show("Enter!");

            //    if (LoginViewModel._ShowMainWindow)
            //    {
            //        MainWindow mainWindow = new MainWindow();
            //        mainWindow.Show();

            //        this.Close();
            //    }
            //}
        }
    }
}
