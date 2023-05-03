using DailyTodo.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyTodo.Repositories
{
    public class UserRepository : RepositoryBase,IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenicateUser(NetworkCredential credential)
        {
            bool validUser;

            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;

                    command.CommandText = "select * from AppUser where Username=@username and UserPassword=@password";
                    command.Parameters.Add("@username", System.Data.SqlDbType.NVarChar).Value = credential.UserName;
                    command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar).Value = credential.Password;

                    validUser = command.ExecuteScalar() == null ? false : true;

                    connection.Close();
                }
            }
            return validUser;
        }

        public void Edit(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            string sqlSelect = $"select * from AppUser";
            int dataCount = -1;
            UserModel user = new UserModel(username);
            DataTable _users = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sqlSelect, connection);
                
                dataCount = adapter.Fill(_users);

                if (dataCount == -1)
                {
                    MessageBox.Show("Table AppUser Adapter.Fill returns -1");
                    return null;
                }

                foreach(DataRow row in _users.Rows)
                {
                    if ((string)row["Username"] == user.Username)
                    {
                        user.UserID = (int)row["UserID"];
                        break;
                    }
                }
            }
            return user;
        }
    }
}
