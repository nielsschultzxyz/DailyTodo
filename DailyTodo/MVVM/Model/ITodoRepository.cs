using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTodo.MVVM.Model
{
    internal interface ITodoRepository
    {
        void InsterNewTodoModel(TodoModel model);
        List<TodoModel> GetAllTodosByUserID(int userID);
        bool EditTodo(TodoModel model);
        void SetTodoAsDone(ObservableCollection<TodoModel> modelList);
    }
}
