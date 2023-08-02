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
            // 这个处理器不会做任何事
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // 这个处理器也不会做任何事
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TableDataGrid.SelectedItem != null)
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
            }
        }

    }

}
