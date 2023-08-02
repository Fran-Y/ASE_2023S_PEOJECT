using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManageClient
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private DataAccessLayer DAL;
        public DataRow DataRow { get; set; }

        public UpdateWindow(DataRow dataRow, DataAccessLayer DAL)
        {
            InitializeComponent();
            DataRow = dataRow;
            this.DAL = DAL;

            // Create a single-row data table from the data row, and bind it to the DataGrid
            DataTable dt = DataRow.Table.Clone();
            dt.ImportRow(DataRow);
            UpdateDataGrid.ItemsSource = dt.DefaultView;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, object> columnValues = DataRow.Table.Columns.Cast<DataColumn>()
                .ToDictionary(column => column.ColumnName, column => DataRow[column]);

            DAL.UpdateRow(DataRow.Table.TableName, columnValues, $"UserID = {DataRow["UserID"]}");

            // TODO: Update the database with the changes
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
