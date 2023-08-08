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
using BookStoreLIB;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for ManagerClient.xaml
    /// </summary>
    public partial class ManagerClient : Window
    {
        private DataAccessLayer DAL;
        public ManagerClient()
        {
            InitializeComponent();
            DAL = new DataAccessLayer(Properties.Settings.Default.ywConnectionString);
            LoadTableNames();
        }

        private void LoadTableNames()
        {
            /*GetTableNames getTableNames = new GetTableNames();
            List<string> tableNames = getTableNames.RetrieveTableNames();*/
            List<string> tableNames = DAL.RetrieveTableNames();

            foreach (string tableName in tableNames)
            {
                TablesComboBox.Items.Add(tableName);
            }
        }

        private void TablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTable = TablesComboBox.SelectedItem.ToString();
            LoadTableData(selectedTable);
        }

        private void LoadTableData(string tableName)
        {
            DataTable dt = DAL.GetDataTable(tableName);
            TableDataGrid.ItemsSource = dt.DefaultView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedTable = TablesComboBox.SelectedItem.ToString();
            DataTable dt = DAL.GetDataTable(selectedTable);

            AddWindow addWindow = new AddWindow(dt, DAL, TablesComboBox.SelectedItem.ToString());
            bool? result = addWindow.ShowDialog();

            if (result == true)
            {
                // Refresh the main window data grid
                LoadTableData(selectedTable);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedTable = TablesComboBox.SelectedItem.ToString();
            DataRowView rowView = (DataRowView)TableDataGrid.SelectedItem;
            DataRow row = rowView.Row;
            if (row != null)
            {
                DataRow selectedRow = row;
                DAL.DeleteRow(selectedTable, selectedRow);

                // Refresh the data grid after deleting the row
                DataTable updatedTable = DAL.GetDataTable(selectedTable);
                TableDataGrid.ItemsSource = updatedTable.DefaultView;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableDataGrid.SelectedItems.Count > 0)
            {
                string selectedTable = TablesComboBox.SelectedItem.ToString();
                DataTable dt = DAL.GetDataTable(selectedTable);

                DataRowView selectedRow = (DataRowView)TableDataGrid.SelectedItems[0];

                UpdateWindow updateWindow = new UpdateWindow(dt, DAL, selectedTable, selectedRow);
                bool? result = updateWindow.ShowDialog();

                if (result == true)
                {
                    // Refresh the main window data grid
                    LoadTableData(selectedTable);
                    //return selectedRow;
                }
            }
            else MessageBox.Show("Please select one item from above.");
        }

    }

}
