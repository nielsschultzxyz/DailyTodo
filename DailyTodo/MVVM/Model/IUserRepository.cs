using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DailyTodo.MVVM.Model
{
    public interface IUserRepository
    {
        bool AuthenicateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(int ID);
        UserModel GetByID(int ID);
        UserModel GetByUsername(string username);
        IEnumerable<UserModel> GetByAll();

    }
}
