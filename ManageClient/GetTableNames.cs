using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageClient
{
    internal class GetTableNames
    {
        public List<string> RetrieveTableNames()
        {
            string connectionString = Properties.Settings.Default.ywConnectionString;
            List<string> tableNames = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableNames.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return tableNames;
        }
    }
}
