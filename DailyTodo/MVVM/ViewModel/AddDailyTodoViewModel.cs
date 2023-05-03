using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DailyTodo.Core;
using DailyTodo.MVVM.Model;
using DailyTodo.Repositories;

namespace DailyTodo.MVVM.ViewModel
{
    internal class AddDailyTodoViewModel : ObservableObject
    {
        // Repos
        public TodoRepository _TodoRepository;
        
        // Fields
        private string _todoname = "Todoname";
        public string Todoname
        {
            get { return (string)_todoname; }
            set
            {
                if(Todoname != value)
                {
                    _todoname = value;
                    OnPropertyChanged(nameof(Todoname));
                }   
            }
        }

        private string _todoDescription = "Description";
        public string TodoDescription
        {
            get { return _todoDescription; }
            set
            {
                if(TodoDescription != value)
                {
                    _todoDescription = value;
                    OnPropertyChanged(nameof(TodoDescription));
                }
            }
        }

        private DateTime _todoFinishedDate = DateTime.Now;
        public DateTime TodoFinishedDate
        {
            get { return _todoFinishedDate; }
            set
            {
                _todoFinishedDate = value;
                //OnPropertyChanged(nameof(TodoFinishedDate));
            }
        }

        private string _isFinished = "Yes / No";
        public string IsFinished
        {
            get { return _isFinished; }
            set
            {
                _isFinished = value;
                OnPropertyChanged(nameof(IsFinished));
            }
        }

        private string _username = $"UerID: {LoginViewModel._AppUser.UserID}";
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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

        // RelayCommands
        public RelayCommand AddNewTodoCommand { get; set; }

        // TodoModel
        public TodoModel InsertTodoModel;
        
        // ctor
        public AddDailyTodoViewModel()
        {
            _TodoRepository = new TodoRepository();
            
            // Insert Command
            AddNewTodoCommand = new RelayCommand(async o => {

                var todoIsFinished = false;
                var insertDate = "";
                DateTime finishedDate;

                if (!String.IsNullOrEmpty(Todoname) && !String.IsNullOrEmpty(TodoDescription))
                {
                    if(Todoname != "Todoname" && TodoDescription != "Description")
                    {
                        if (IsFinished.ToLower() == "yes")
                            todoIsFinished = true;

                        insertDate = TodoFinishedDate.ToShortDateString();
                        DateTime.TryParse(insertDate, out finishedDate);

                        InsertTodoModel = new TodoModel(Todoname, TodoDescription, DateTime.Now, todoIsFinished, TodoFinishedDate);

                        var insertTask = Task.Run(() => {
                            _TodoRepository.InsterNewTodoModel(InsertTodoModel);
                        });
                        await insertTask;

                        MessageBox.Show($"New Todo has been added({InsertTodoModel.Todoname})!");

                        Todoname = "Set another Todoname";
                        TodoDescription = "Todo Description";
                        TodoFinishedDate = DateTime.Now;
                        IsFinished = "Yes / No";
                        ErrorMessage = "Passt";
                    }
                }
                else
                {
                    ErrorMessage = "Please check your input, all fields must be filled with data!" + Environment.NewLine + "(Todoname, Description, Date)";
                    return;
                }
            });
        }
    }
}
