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
    internal class TodoViewModel : ObservableObject
    {
        // repo
        public TodoRepository _todoRepository;
        // collection
        private ObservableCollection <TodoModel> _todoModelCollection { get; set; }

        // fields
        private string _todoname = "Todoname";
        public string Todoname
        {
            get { return _todoname; }
            set
            {
                if(_todoname != value)
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
                if(_todoDescription != value)
                {
                    _todoDescription = value;
                    OnPropertyChanged(nameof(TodoDescription));
                }
            }
        }

        private DateTime _todoCreateDate = DateTime.Now;
        public DateTime TodoCreateDate
        {
            get { return _todoCreateDate; }
            set
            {
                _todoCreateDate = value;
            }
        }

        private bool _isTodoFinished = false;
        public bool IsTodoFinished
        {
            get { return _isTodoFinished; }
            set
            {
                if(_isTodoFinished != value)
                {
                    _isTodoFinished = value;
                    OnPropertyChanged(nameof(IsTodoFinished));
                }
            }
        }

        private DateTime _todoFinsishedTillDate = DateTime.Now;
        public DateTime TodoFinsishedTillDate
        {
            get { return _todoFinsishedTillDate; }
            set
            {
                _todoFinsishedTillDate = value;
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

        // ctor
        public TodoViewModel()
        {
            SelectedTodoModel = new TodoModel("", "", DateTime.Now, false, DateTime.Now);
            Todoname = SelectedTodoModel.Todoname;
            //if(MainWindow.TodoModel.Todoname != "")
            //{
            //    Todoname = MainWindow.TodoModel.Todoname;
            //    TodoDescription = MainWindow.TodoModel.TaskDescription;
            //    TodoCreateDate = MainWindow.TodoModel.CreateDate;
            //    IsTodoFinished = MainWindow.TodoModel.IsTodoFinished;
            //    TodoFinsishedTillDate = MainWindow.TodoModel.FinishedTillDate;
            //}
        }
    }
}
