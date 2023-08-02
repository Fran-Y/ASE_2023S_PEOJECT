using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageClient
{
    public class DataAccessLayer
    {
        private string connectionString;

        public DataAccessLayer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void UpdateRow(string tableName, Dictionary<string, object> columnValues, string whereCondition)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string setClause = string.Join(", ", columnValues.Keys.Select(column => $"{column} = @{column}"));
                string query = $"UPDATE {tableName} SET {setClause} WHERE {whereCondition}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var item in columnValues)
                    {
                        command.Parameters.AddWithValue("@" + item.Key, item.Value);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<string> RetrieveTableNames()
        {
            List<string> tableNames = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                DataTable schema = connection.GetSchema("Tables");

                foreach (DataRow row in schema.Rows)
                {
                    string tableName = (string)row[2];
                    tableNames.Add(tableName);
                }
            }

            return tableNames;
        }

        public DataTable GetDataTable(string tableName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection);
                dataAdapter.Fill(dt);
            }

            return dt;
        }
    }

}
