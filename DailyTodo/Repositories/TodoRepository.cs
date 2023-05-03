using DailyTodo.MVVM.Model;
using DailyTodo.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyTodo.Repositories
{
    internal class TodoRepository : RepositoryBase, ITodoRepository
    {
        public bool EditTodo(TodoModel model)
        {
            throw new NotImplementedException();
        }

        public List<TodoModel> GetAllTodosByUserID(int UserID)
        {
            int dataCount = -1;

            DataTable UserTodosDataTable = new DataTable();
            string sqlSelect = $"SELECT * FROM DailyTodo WHERE UserID = {UserID}";

            List<TodoModel> currentTodos = new List<TodoModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
            
                adapter.SelectCommand = new SqlCommand(sqlSelect, connection);
                dataCount = adapter.Fill(UserTodosDataTable);

                if(dataCount != -1)
                {
                    var format = string.Format("dd-MM-yyyy");
                    var createDate = DateTime.Now.ToShortDateString();
                    var finishedDate = DateTime.Now.ToShortDateString();
                    bool isFinished = false;

                    foreach(DataRow row in UserTodosDataTable.Rows)
                    {
                        if ((int)row["IsTodoFinished"] == 1)
                        {
                            isFinished = true;
                        }

                        currentTodos.Add(new TodoModel((string)row["Todoname"], (string)row["TaskDescription"], 
                            (DateTime)row["CreateDate"], isFinished, (DateTime)row["FinishedTillDate"]));

                        isFinished = false;
                    }
                }
            }
            return currentTodos;
        }

        public void InsterNewTodoModel(TodoModel model)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new SqlCommand())
                    {
                        // format string -> sqlformt
                        var format = "yyyy-MM-dd";

                        connection.Open();
                        command.Connection = connection;

                        MessageBox.Show($"Current UserID\nAppUserID: {LoginViewModel._AppUser.UserID}");

                        // Static User -> Get the userID in terms to add new Todos into SQL-Table (DailyTodo) 
                        command.CommandText = $"INSERT INTO DailyTodo " +
                            $"(UserID, Todoname, TaskDescription, CreateDate, IsTodoFinished, ClosedDate, FinishedTillDate) " +
                                $"VALUES" +
                                $"(" +
                                    $"@UserID, " +
                                    $"@Todoname, " +
                                    $"@TaskDescription, " +
                                    $"@CreateDate, " +
                                    $"@IsTodoFinished, " +
                                    $"NULL, " +
                                    $"@FinishedTillDate" +
                                $");";

                        // Values
                        command.Parameters.AddWithValue("@UserID", LoginViewModel._AppUser.UserID);
                        command.Parameters.AddWithValue("@Todoname", model.Todoname);
                        command.Parameters.AddWithValue("@TaskDescription", model.TaskDescription);
                        command.Parameters.AddWithValue("@CreateDate", DateTime.Now.ToString(format));
                        command.Parameters.AddWithValue("@IsTodoFinished", model.IsTodoFinished);
                        command.Parameters.AddWithValue("@FinishedTillDate", model.FinishedTillDate.ToString(format));

                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"ERROR MESSAGE: {ex.Message}");
            }
        }

        // TODO: command values new define errormsg values are already defined?! 
        public void SetTodoAsDone(ObservableCollection<TodoModel> todoModelList)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;

                        command.CommandText = $"UPDATE DailyTodo SET IsTodoFinished = 1" +
                            $"WHERE " +
                                $"UserID = @UserIDUpdate AND " +
                                $"Todoname = @TodonameUpdate AND " +
                                $"FinishedTillDate = @FinishedTillDateUpdate;";

                        foreach (TodoModel model in todoModelList)
                        {
                            if (model.IsTodoFinished == true)
                            {
                                command.Parameters.AddWithValue("@UserIDUpdate", LoginViewModel._AppUser.UserID);
                                command.Parameters.AddWithValue("@TodonameUpdate", model.Todoname);
                                command.Parameters.AddWithValue("@FinishedTillDateUpdate", model.FinishedTillDate);

                                command.ExecuteNonQuery();
                            }
                        }

                        connection.Close();
                    }
                }
                MessageBox.Show("Changes have been saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR MSG:\n{ex.Message}");
            }
        }
    }
}
