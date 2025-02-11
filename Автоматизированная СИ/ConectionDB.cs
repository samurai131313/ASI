using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace Автоматизированная_СИ
{
    internal class ConectionDB
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source = klaus\SQLEXPRESS; Initial Catalog=inventory;Integrated Security=True");

        public void openConnection()
        {
            if(sqlConnection.State == System.Data.ConnectionState.Closed) { sqlConnection.Open(); }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open) { sqlConnection.Close(); }
        }
        public SqlConnection GetConnection() { return sqlConnection; }
    }
}
