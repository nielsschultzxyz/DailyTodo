using DailyTodo.Core;
using DailyTodo.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.Common;
using DailyTodo.Repositories;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Windows;

namespace DailyTodo.MVVM.ViewModel
{
    internal class DailyTodoViewModel : ObservableObject
    {
        public Func<DateTime, DateTime> GetDate = (a) => a; // (EndDate - StartDate).TotalDays
        public DateTime FilterListDate { get; set; } // filter Date -> btnClick (Yesterday/Tomorrow)
        // repos
        public TodoRepository _todoRepository;

        // fields
        private string _currentDateTodo = "Todos for yyyy.MM.dd";
        public string CurrentDateTodo
        {
            get { return _currentDateTodo; }
            set
            {
                if(_currentDateTodo != value)
                {
                    _currentDateTodo = value;
                    OnPropertyChanged(nameof(CurrentDateTodo));
                }
            }
        }

        private string _itemsFoundMsg = "";
        public string ItemsFoundMsg
        {
            get { return _itemsFoundMsg; }
            set
            {
                if(_itemsFoundMsg != value)
                {
                    _itemsFoundMsg = value;
                    OnPropertyChanged(nameof(ItemsFoundMsg));
                }
            }
        }

        // collection
        private ObservableCollection<TodoModel> _todosCollection;
        public ObservableCollection<TodoModel> TodoCollection
        {
            get { return _todosCollection; }
            set
            {
                _todosCollection = value; 
                OnPropertyChanged(nameof(TodoCollection));
            }
        }

        // RelayCommands
        public RelayCommand ShowYesterdayTodoCommand { get; set; }
        public RelayCommand ShowTomorrowTodoCommand { get; set; }
        public RelayCommand SaveTodoChangesCommand { get; set; }
        public RelayCommand ShowAddNewTodoViewCommand { get; set; }

        // ctor
        public DailyTodoViewModel() 
        {
            // repo
            _todoRepository = new TodoRepository();

            // set currentDate value
            CurrentDateTodo = $"Todos for {DateTime.Now.DayOfWeek} || {DateTime.Now.ToShortDateString()}";

            TodoCollection = new ObservableCollection<TodoModel>();
            FilterListDate = DateTime.Now;

            //Task.Run(() => {
                FilterListDate = DateTime.Now;
                TodoCollection = filterCollectionByDate(TodoCollection, GetDate(DateTime.Now));
            //});
            
            ShowTomorrowTodoCommand = new RelayCommand(o => {
                TodoCollection = filterCollectionByDate(TodoCollection, GetDate(FilterListDate.AddDays(1)));
            });

            ShowYesterdayTodoCommand = new RelayCommand(o => {
                TodoCollection = filterCollectionByDate(TodoCollection, GetDate(FilterListDate.AddDays(-1)));
            });

            // TODO: TodoRepository -> set finished = true if the checkbox for the checkbox items ischecked loop trough the filterlist for the date(day)
            SaveTodoChangesCommand = new RelayCommand(o => {
                _todoRepository.SetTodoAsDone(TodoCollection);
            });

            // Change View (AddDailyTodosView)
            ShowAddNewTodoViewCommand = MainViewModel.ShowAddDailyTodoViewCommand;
        }

        // Methode
        private ObservableCollection<TodoModel> getTodoModels(ObservableCollection<TodoModel> modelCollection)
        {
            _todoRepository = new TodoRepository();
            List<TodoModel> todoModelsListGetAllTodos = new List<TodoModel>();

            // GetAllTodosByUserID
            todoModelsListGetAllTodos = _todoRepository.GetAllTodosByUserID(LoginViewModel._AppUser.UserID);

            // Add Model
            foreach(TodoModel model in todoModelsListGetAllTodos)
            {
                modelCollection.Add(model);
            }
            return modelCollection;
        }

        private ObservableCollection<TodoModel> filterCollectionByDate(ObservableCollection<TodoModel> todoModelList, DateTime filterListDate)
        {
            // Set filterListDate
            FilterListDate = filterListDate;
            CurrentDateTodo = $"Todos for {filterListDate.DayOfWeek} || {filterListDate.ToShortDateString()}";

            todoModelList = getTodoModels(todoModelList);
            ObservableCollection<TodoModel> filterCollection = new ObservableCollection<TodoModel>();
            
            // get all todos where the current == todo date
            foreach (TodoModel model in todoModelList)
            {
                if (model.FinishedTillDate.ToShortDateString() == filterListDate.ToShortDateString())
                {
                    filterCollection.Add(model);
                }
            }

            if (filterCollection.Count() == 0)
                ItemsFoundMsg = $"{filterListDate.DayOfWeek} - {filterListDate.ToShortDateString()}\nThere are no todos set/planed yet!";
            else
                ItemsFoundMsg = "";

            return filterCollection;
        }
    }
}
