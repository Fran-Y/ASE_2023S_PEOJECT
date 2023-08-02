using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManageClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataAccessLayer DAL;
        public MainWindow()
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

        /*function no done*/
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
/*            if (TableDataGrid.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)TableDataGrid.SelectedItem;
                DataRow row = rowView.Row;
                UpdateWindow updateWindow = new UpdateWindow(row, DAL);
                bool? result = updateWindow.ShowDialog();

                if (result == true)
                {
                    // TODO: Refresh the main window data grid
                    LoadTableData(TablesComboBox.SelectedItem.ToString());
                }
            }*/
        }

    }

}
