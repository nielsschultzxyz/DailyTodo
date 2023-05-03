using DailyTodo.MVVM.Model;
using DailyTodo.MVVM.ViewModel;
using DailyTodo.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DailyTodo
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //internal static TodoModel TodoModelSelected { get; set; }
        public MainWindow()
        {
            //TodoModelSelected = new TodoModel("Todoname_todos_dosdos", "TaskDescription", DateTime.Now, false, DateTime.Now);
            InitializeComponent();
            Application.Current.MainWindow = this;
        }

        //private void ListView_Selected(object sender, RoutedEventArgs e)
        //{
        //    if (TodoListView.SelectedIndex != -1)
        //    {
        //        foreach(TodoModel model in MainViewModel.TodoModelCollection)
        //        {
        //            if(model.Todoname == TodoListView.SelectedItem.ToString())
        //            {
        //                MessageBox.Show("hier passiert was!");

        //                TodoModel = model;
        //            }
        //        }
        //    }
        //}

        //private void isItemSelected()
        //{
        //    foreach(ListBoxItem item in TodoListView.Items)
        //    {
        //        if(item.IsSelected == true)
        //        {
        //            foreach(TodoModel model in MainViewModel.TodoModelCollection)
        //            {
        //                if(item.ToString() == model.Todoname)
        //                {
        //                    MainViewModel.Selected = model;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
