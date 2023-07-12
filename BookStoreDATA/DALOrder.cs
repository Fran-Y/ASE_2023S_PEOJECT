/* ************************************************************
 * For students to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * ************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreLIB {
    public class DALOrder {
        public int Proceed2Order(string xmlOrder) {
            SqlConnection cn = new SqlConnection(
                BookStoreDATA.Properties.Settings.Default.xyConnectionString);
            try {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insertOrder";
                SqlParameter inParameter = new SqlParameter();
                inParameter.ParameterName = "@xmlOrder";
                inParameter.Value = xmlOrder;
                inParameter.DbType = DbType.String;
                inParameter.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(inParameter);
                SqlParameter ReturnParameter = new SqlParameter();
                ReturnParameter.ParameterName = "@OrderID";
                ReturnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(ReturnParameter);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                return (int)cmd.Parameters["@OrderID"].Value;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
            finally {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
    }
}
