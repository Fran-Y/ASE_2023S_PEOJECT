using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace UnitTestLoginPage
{
    public class DALUserInfo
    {
        public int LogIn(string userName, string password) {
            var conn = new SqlConnection(BookStoreDATA.Properties.Settings.Default.xyConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select UserID from UserData where" 
                    + " UserName = @UserName and Password = @Password ";
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                //Console.WriteLine(userName+password);
                conn.Open();
                int userId = (int)cmd.ExecuteScalar();
                //Console.WriteLine("++++++++++"+userId);
                if (userId > 0)
                {
                    //Console.WriteLine("++++++++++" + userId);
                    return userId;
                }
                else {
                    //Console.WriteLine("++++++++++" + userId);
                    return -1; }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally{
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        /*public DALUserInfo()
        {
        }*/
    }
}