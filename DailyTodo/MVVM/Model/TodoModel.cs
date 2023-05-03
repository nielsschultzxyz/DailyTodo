using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using DailyTodo.Core;
using DailyTodo.MVVM.ViewModel;
using DailyTodo.Repositories;

namespace DailyTodo.MVVM.Model
{
    internal class TodoModel : Repositories.RepositoryBase
    {
        public static TodoRepository _todoRepository;
        // Fields
        public string Todoname { get; set; }
        public string TaskDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsTodoFinished { get; set; } // TODO: change bool
        public DateTime FinishedTillDate { get; set; }
        public DateTime ClosedDate { get; set; }

        // RelayCommands
        //public RelayCommand InsertNewTodoCommand { get; set; }

        // ctor
        public TodoModel(string Todoname, string TaskDescription, DateTime CreateDate, bool IsTodoFinished, DateTime FinishedTillDate)
        {
            _todoRepository = new TodoRepository();

            this.Todoname = Todoname;
            this.TaskDescription = TaskDescription;
            this.CreateDate = CreateDate;
            this.IsTodoFinished = IsTodoFinished;
            this.FinishedTillDate = FinishedTillDate;

            //InsertNewTodoCommand = new RelayCommand(o => { _todoRepository.InsterNewTodoModel(this); });
        }

        //public void InsertNewTodo(TodoModel model)
        //{
        //    using (var connection = GetConnection())
        //    {
        //        using (var command = new SqlCommand())
        //        {
        //            var insertTask = Task.Run(() => {
        //                connection.Open();
        //                command.Connection = connection;
        //                // TODO -> Syntax error near WHERE fehler bei der UserID? static user?
        //                command.CommandText = $"INSERT INTO DailyTodo " +
        //                    $"WHERE UserID = {LoginViewModel._AppUser.UserID} " +
        //                    $"(Todoname, TaskDescription, CreateDate, IsTodoFinished, FinishedTillDate, ClosedDate) " +
        //                        $"VALUES" +
        //                        $"(" +
        //                            $"{model.Todoname}, {model.TaskDescription}, {model.CreateDate}, {model.IsTodoFinished}, {model.FinishedTillDate}, NULL" +
        //                        $")";

        //                command.ExecuteNonQuery();
        //                connection.Close();
        //            });
        //        }
        //    }
        //}
    }
}
