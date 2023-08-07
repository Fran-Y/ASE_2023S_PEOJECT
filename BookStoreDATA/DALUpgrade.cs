using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDATA
{
    public class DALUpgrade
    {

        private readonly string connectionString = BookStoreDATA.Properties.Settings.Default.xyConnectionString;
        public void UpdateLine(string tableName, string rowId, DataRow updatedRow, string whereCondition)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                StringBuilder queryBuilder = new StringBuilder($"UPDATE [dbo].[{tableName}] SET ");

                List<string> skipColumns = new List<string>
        {
            "OrderID", "ISBN", "FeedbackID", "UserID","CategoryID","Name","Ccode","UserNmae","Password","Type","Manager","SupplierId","InStock","OrderDate"
            //GetIdColumnNameByTableName(tableName) //
        };

                foreach (DataColumn column in updatedRow.Table.Columns)
                {
                    if (skipColumns.Contains(column.ColumnName))
                        continue;
                    object value = updatedRow[column.ColumnName];
                    if (value == DBNull.Value)
                    {
                        queryBuilder.Append($"[{column.ColumnName}] = NULL, ");
                    }
                    else if (value is string || value is DateTime)
                    {
                        queryBuilder.Append($"[{column.ColumnName}] = '{value}', ");
                    }
                    else
                    {
                        queryBuilder.Append($"[{column.ColumnName}] = {value}, ");
                    }
                }

                queryBuilder.Length -= 2;  // Remove the trailing comma and space
                queryBuilder.Append($" WHERE {whereCondition}");
                Debug.WriteLine(queryBuilder.ToString());


                SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;  // Optionally rethrow the exception if needed
                }
            }
        }
    }
}
