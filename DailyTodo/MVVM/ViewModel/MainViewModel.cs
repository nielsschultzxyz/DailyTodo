using DailyTodo.Core;
using DailyTodo.MVVM.Model;
using DailyTodo.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyTodo.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        // repos
        public TodoRepository _todoRepository;

        // fields
        private object _currentView;
        public object CurrentView 
        { 
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            } 
        }

        private TodoModel _selectedTodoModel;
        public TodoModel SelectedTodoModel
        {
            get { return (TodoModel)_selectedTodoModel; }
            set
            {
                _selectedTodoModel = value;
                OnPropertyChanged(nameof(SelectedTodoModel));
            }
        }
        // Todos Collection
        private ObservableCollection<TodoModel> _todoModelCollection { get; set; }
        public ObservableCollection <TodoModel> TodoModelCollection
        {
            get { return _todoModelCollection; }
            set
            {
                _todoModelCollection = value;
                OnPropertyChanged();
            }
        }

        public List<TodoModel> TodoModelList { get; set; }

        // Vms
        public DailyTodoViewModel DailyTodoVM { get; set; }
        public DashboardViewModel DashboardVM { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public NotifyViewModel NotifyVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public TodoViewModel TodoVM { get; set; }
        public AddDailyTodoViewModel AddDailyTodoVM { get; set; }

        // RelayCommands
        public RelayCommand ShowDailyTodoViewCommand { get; set; }
        public RelayCommand ShowDashboardViewCommad { get; set; }
        public RelayCommand ShowHomeViewCommand { get; set; }
        public RelayCommand ShowNotifyViewCommand { get; set; }
        public RelayCommand ShowSettingsViewCommand { get; set; }
        public RelayCommand ShowTodoViewCommand { get; set; }
        public static RelayCommand ShowAddDailyTodoViewCommand { get; set; }

        // TitelBar Commands
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }

        // ctor
        public MainViewModel()
        {
            // VMs
            DailyTodoVM = new DailyTodoViewModel();
            DashboardVM = new DashboardViewModel();
            HomeVM = new HomeViewModel();
            NotifyVM = new NotifyViewModel();
            SettingsVM = new SettingsViewModel();
            TodoVM = new TodoViewModel();
            AddDailyTodoVM = new AddDailyTodoViewModel();
            CurrentView = HomeVM;

            // repo
            _todoRepository = new TodoRepository();

            // TodoCollection
            TodoModelCollection = new ObservableCollection<TodoModel>();
            TodoModelList = new List<TodoModel>();
            
            Task.Run(() => {
                TodoModelList = _todoRepository.GetAllTodosByUserID(LoginViewModel._AppUser.UserID);
                foreach (TodoModel model in TodoModelList)
                {
                    TodoModelCollection.Add(model);
                }
            });
            // Add new Items into the Collection test
            //TodoModelCollection.Add(new TodoModel("MainViewModel", "Beschreibung", DateTime.Now, 0, DateTime.Now));

            // Window Commands
            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });
            ShutDownWindowCommand = new RelayCommand(o => { Application.Current.Shutdown(); });
            MinimizeWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });

            MaximizeWindowCommand = new RelayCommand(o => {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
            });


            // Set CurrentView
            ShowHomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            ShowDailyTodoViewCommand = new RelayCommand(o => { CurrentView = DailyTodoVM; });
            ShowDashboardViewCommad = new RelayCommand(o => { CurrentView = DashboardVM; });
            ShowNotifyViewCommand = new RelayCommand(o => { CurrentView = NotifyVM; });
            ShowSettingsViewCommand = new RelayCommand(o => { CurrentView = SettingsVM; });
            ShowTodoViewCommand = new RelayCommand(o => { CurrentView = TodoVM; });
            ShowAddDailyTodoViewCommand = new RelayCommand(o => { CurrentView = AddDailyTodoVM; });            
        }
    }
}
