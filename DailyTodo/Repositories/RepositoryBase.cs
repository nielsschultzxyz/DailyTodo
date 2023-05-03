using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyTodo.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase()
        {
            _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFilename={GetConnectionString()};Integrated Security=true";
        }

        protected SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(_connectionString);
            }
            catch(Exception ex)
            {
                throw new Exception($"Was tun? {ex.Message}");
            }
        }

        private string GetConnectionString()
        {
            var _connectionString = "";
            var readerPath = @"D:\C# Training\DailyTodo\DatabaseConnectionString.txt";
            
            try
            {
                Task.Run(() => {
                    using (StreamReader reader = new StreamReader(readerPath))
                    {
                        while(reader.ReadLine() != null)
                        {
                            _connectionString += reader.ReadLine();
                        }
                    }
                });
                return _connectionString;
            }
            catch(IOException ioex)
            {
                // TODO ioex errorlog -> MessageBox for now
                MessageBox.Show($"Es ist folgender Fehler aufgetreten: {ioex.Message}");
            }
            return null;
        }

    }
}
