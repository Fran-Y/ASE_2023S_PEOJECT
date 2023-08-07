using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace UnitTestLoginPage
{
    public class DALUserInfo
    {
        public LoginResult LogIn(string userName, string password)
        {
            var conn = new SqlConnection(BookStoreDATA.Properties.Settings.Default.xyConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select UserID, Manager from UserData where"
                    + " UserName = @UserName and Password = @Password ";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(0);
                        bool isManager = reader.GetBoolean(1);  // Assuming that Manager column is a boolean type
                                                                // return a new LoginResult with userId and isManager
                        return new LoginResult
                        {
                            UserId = userId,
                            IsManager = isManager
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        /*public DALUserInfo()
        {
        }*/
    }
}