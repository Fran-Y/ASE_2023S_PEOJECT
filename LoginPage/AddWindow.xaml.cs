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

            foreach (KeyValuePair<string, TextBox> entry in _inputFields)
            {
                string columnName = entry.Key;
                TextBox textBox = entry.Value;

                // Check the data type
                Type expectedType = _table.Columns[columnName].DataType;
                if (!IsValidDataType(textBox.Text, expectedType))
                {
                    MessageBox.Show($"Invalid data type for {columnName}. Expected {expectedType.Name}.");
                    return; // Exit without saving
                }

                // Convert input to its proper type before saving to the DataRow
                if (expectedType == typeof(int))
                {
                    row[columnName] = int.Parse(textBox.Text);
                }
                else if (expectedType == typeof(decimal))
                {
                    row[columnName] = decimal.Parse(textBox.Text);
                }
                else
                {
                    row[columnName] = textBox.Text;
                }
            }

            // If all data types are valid, proceed with the save
            _dal.AddRow(_tableName, row);
            this.DialogResult = true;
        }

        private bool IsValidDataType(string input, Type expectedType)
        {
            if (expectedType == typeof(string))
            {
                return true; // Always valid for strings
            }
            else if (expectedType == typeof(int))
            {
                return int.TryParse(input, out _);
            }
            else if (expectedType == typeof(decimal))
            {
                return decimal.TryParse(input, out _);
            }
            else if (expectedType == typeof(double))
            {
                return double.TryParse(input, out _);
            }
            else if (expectedType == typeof(DateTime))
            {
                return DateTime.TryParse(input, out _);
            }
            else if (expectedType == typeof(char))
            {
                return char.TryParse(input, out _);
            }
            else if (expectedType == typeof(bool))
            {
                return bool.TryParse(input, out _);
            }
            // Add more type checks as needed

            // By default, return false
            return false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving any changes
            this.DialogResult = false;
        }

    }
}
