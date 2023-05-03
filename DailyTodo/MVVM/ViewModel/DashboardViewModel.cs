using DailyTodo.Core;
using DailyTodo.MVVM.Model;
using DailyTodo.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTodo.MVVM.ViewModel
{
    internal class DashboardViewModel : ObservableObject
    {
        // Dalegate
        private Func<DateTime, DateTime> getFilterDateTime = (x) => x; 
        // repo
        private TodoRepository _todoRepository;

        // fields
        private string _counterForAllTodos = "";
        public string CounterForAllTodos
        {
            get { return _counterForAllTodos; }
            set
            {
                _counterForAllTodos = value;
                OnPropertyChanged();
            }
        }

        private string _finishedTodos = "";
        public string FinishedTodos
        {
            get { return _finishedTodos; }
            set
            {
                _finishedTodos = value;
                OnPropertyChanged();
            }
        }

        private string _todosStillOpen = "";
        public string TodosStillOpen
        {
            get { return _todosStillOpen; }
            set
            {
                _todosStillOpen = value;
                OnPropertyChanged();
            }
        }

        private string _finishedTodosPercent = "";
        public string FinishedTodosPercent
        {
            get { return _finishedTodosPercent; }
            set
            {
                _finishedTodosPercent = value;
                OnPropertyChanged();
            }
        }

        // Collection
        public ObservableCollection<TodoModel> TodoModelsCollection { get; set; }
        
        private List<TodoModel> _todoModelList;
        public List<TodoModel> TodoModelList
        {
            get { return _todoModelList; }
            set
            {
                _todoModelList = value;
                OnPropertyChanged();
            }
        }

        // RelayCommand
        public RelayCommand filterForDayCommand { get; set; }
        public RelayCommand filterForWeekCommand { get; set; }
        public RelayCommand filterForMonthCommand { get; set; }
        public RelayCommand filterForYearCommand { get; set; }
        public RelayCommand filterForAllCommand { get; set; }

        // ctor
        public DashboardViewModel()
        {
            _todoRepository = new TodoRepository();

            TodoModelList = new List<TodoModel>();
            TodoModelList = _todoRepository.GetAllTodosByUserID(LoginViewModel._AppUser.UserID);

            // Commands
            filterForDayCommand = new RelayCommand(o => {
                filterTodoModelListDate(DateTime.Now, DateTime.Now);
            });

            filterForWeekCommand = new RelayCommand(o => {
                filterTodoModelListDate(DateTime.Now, getFilterDateTime(DateTime.Now.AddDays(-7)));
            });

            filterForMonthCommand = new RelayCommand(o => {
                filterTodoModelListDate(DateTime.Now, getFilterDateTime(DateTime.Now.AddMonths(-1)));
            });

            filterForYearCommand = new RelayCommand(o => {
                filterTodoModelListDate(DateTime.Now, getFilterDateTime(DateTime.Now.AddYears(-1)));
            });

            filterForAllCommand = new RelayCommand(o => {
                filterTodoModelListDate(DateTime.Now, getFilterDateTime(DateTime.Now.AddYears(-99)));
            });
        }

        private List<TodoModel> filterTodoModelListDate(DateTime currentDate, DateTime filterDate)
        {
            List<TodoModel> filterList = new List<TodoModel>();
            
            int todoFinishedCounter = 0, todoStillOpen = 0;
            double todoFinishedTodoPercent = 0.00;

            if(TodoModelList.Count() > 0)
            {
                // filter todolist
                foreach (TodoModel model in TodoModelList)
                {
                    if (model.FinishedTillDate.Date <= currentDate.Date && model.FinishedTillDate.Date >= filterDate.Date)
                    {
                        filterList.Add(model);
                    }
                }

                // get field values
                foreach(TodoModel model in filterList)
                {
                    if(model.IsTodoFinished == true)
                        todoFinishedCounter++;
                }

                todoStillOpen = filterList.Count() - todoFinishedCounter;

                if(todoStillOpen > 0)
                    todoFinishedTodoPercent = (todoStillOpen * 100) / filterList.Count;

                // set value
                CounterForAllTodos = filterList.Count().ToString();
                FinishedTodos = todoFinishedCounter.ToString();
                TodosStillOpen = todoStillOpen.ToString();
                FinishedTodosPercent = todoFinishedTodoPercent.ToString();
            }
            
            return filterList;
        }
    }
}
