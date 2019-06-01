using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Contacts
{
    class Connect
    {
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=roots;password=;database=contacts");

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void openConnection()
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if(connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
