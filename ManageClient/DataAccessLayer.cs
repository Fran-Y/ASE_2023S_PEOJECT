﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace ManageClient
{
    public class DataAccessLayer
    {
        private SqlConnection _connection;
        private string connectionString;

        public DataAccessLayer(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
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

        public void AddRow(string tableName, DataRow row)
        {
            // Start by building a SQL command
            StringBuilder sqlBuilder = new StringBuilder($"INSERT INTO [dbo].[{tableName}] (");


            // Add the column names to the command
            foreach (DataColumn column in row.Table.Columns)
            {
                sqlBuilder.Append($"[{column.ColumnName}], ");
            }

            // Remove the trailing comma and space, then add the VALUES keyword
            sqlBuilder.Length -= 2;
            sqlBuilder.Append(") VALUES (");

            // Add the values to the command
            foreach (object value in row.ItemArray)
            {
                if (value == DBNull.Value)
                {
                    sqlBuilder.Append("NULL, ");
                }
                else if (value is string || value is DateTime)
                {
                    // Strings and DateTime values need to be enclosed in single quotes
                    sqlBuilder.Append($"'{value}', ");
                }
                else
                {
                    // Other types can be added as they are
                    sqlBuilder.Append($"{value}, ");
                }
            }

            // Remove the trailing comma and space, then add a closing parenthesis
            sqlBuilder.Length -= 2;
            sqlBuilder.Append(")");

            // Now execute the command
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlBuilder.ToString(), connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRow(string tableName, DataRow row)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 创建一个 WHERE 子句，它将所有列的值都视为删除行的条件
                string whereClause = string.Join(" AND ", row.Table.Columns.Cast<DataColumn>().Select(column => $"{column.ColumnName} = @{column.ColumnName}"));

                string query = $"DELETE FROM [dbo].[{tableName}] WHERE {whereClause}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // 为每个参数添加一个值
                    foreach (DataColumn column in row.Table.Columns)
                    {
                        command.Parameters.AddWithValue($"@{column.ColumnName}", row[column.ColumnName]);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

    }

}
