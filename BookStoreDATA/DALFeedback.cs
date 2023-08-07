using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace UnitTestLoginPage
{
    public class DALFeedback
    {
        private readonly string connectionString = BookStoreDATA.Properties.Settings.Default.xyConnectionString;

        public bool SaveFeedback(Feedback feedback)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Feedback(ISBN, UserID, Content) VALUES(@ISBN, @UserID, @Content)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", feedback.ISBN);
                cmd.Parameters.AddWithValue("@UserID", feedback.UserID);
                cmd.Parameters.AddWithValue("@Content", feedback.Content);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;  // If any rows were affected, the feedback was saved successfully
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
            }
        }


        public string GetFeedback(string isbn)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Content FROM Feedback WHERE ISBN = @ISBN";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", isbn);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return result.ToString();  // If any feedback exists, return the content of the first one
                    }
                    else
                    {
                        return null;  // If no feedback exists, return null
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return null;
                }
            }
        }
    }
}
