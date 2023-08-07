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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private DataTable _table;
        private DataAccessLayer _dal;
        private Dictionary<string, TextBox> _inputFields;
        private string _tableName;

        public AddWindow(DataTable table, DataAccessLayer dal, string tableName)
        {
            InitializeComponent();

            this.Title = tableName;

            _dal = dal;
            _table = table;
            _inputFields = new Dictionary<string, TextBox>();
            _tableName = tableName;

            foreach (DataColumn column in _table.Columns)
            {
                // Create a label for the column name and type
                Label label = new Label();
                label.Content = $"{column.ColumnName} ({column.DataType.Name})";

                // Create an input field for the column
                TextBox textBox = new TextBox();
                _inputFields[column.ColumnName] = textBox;

                // Add the label and input field to the panel
                InputFieldsPanel.Children.Add(label);
                InputFieldsPanel.Children.Add(textBox);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new row
            DataRow row = _table.NewRow();

            // Get the data from the input fields and save it to the row
            foreach (KeyValuePair<string, TextBox> entry in _inputFields)
            {
                string columnName = entry.Key;
                TextBox textBox = entry.Value;
                row[columnName] = textBox.Text;
            }

            // Finally, add the row to the table
            _dal.AddRow(_tableName, row);
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving any changes
            this.DialogResult = false;
        }

    }
}
