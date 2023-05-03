using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DailyTodo.MVVM.Model
{
    public class UserModel : Repositories.RepositoryBase
    {
        // fields
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool ValidUser { get; set; }

        // Ctor UserModel
        public UserModel(string username)
        {
            Username = username;
        }

        // Database Methoden
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
    }
}
