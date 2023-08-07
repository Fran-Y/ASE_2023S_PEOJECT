using BookStoreLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using BookStoreDATA;


namespace BookStoreGUI
{
   
    public partial class UpdateWindow : Window
    {
        private DataTable _table;
        private DataAccessLayer _dal;
        private Dictionary<string, TextBox> _inputFields;
        private string _tableName;
        private DataRow _selectedRow;
        DALUpgrade dAL = new DALUpgrade();
        public UpdateWindow(DataTable table, DataAccessLayer dal, string tableName, DataRowView selectedRow)
        {
            InitializeComponent();

            this.Title = tableName;

            _dal = dal;
            _table = table;
            _inputFields = new Dictionary<string, TextBox>();
            _tableName = tableName;
            _selectedRow = selectedRow.Row;

            foreach (DataColumn column in _table.Columns)
            {
                // Create a label for the column name and type
                Label label = new Label();
                label.Content = $"{column.ColumnName} ({column.DataType.Name})";

                // Create an input field for the column
                TextBox textBox = new TextBox();

                textBox.Text = selectedRow.Row[column.ColumnName].ToString(); // fill the textbox with the selected row data

                _inputFields[column.ColumnName] = textBox;

                // Add the label and input field to the panel
                InputFieldsPanel.Children.Add(label);
                InputFieldsPanel.Children.Add(textBox);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Update the selected row
            Dictionary<string, object> columnValues = new Dictionary<string, object>();

            foreach (KeyValuePair<string, TextBox> entry in _inputFields)
            {
                string columnName = entry.Key;
                TextBox textBox = entry.Value;
                columnValues[columnName] = textBox.Text;
                _selectedRow[columnName] = textBox.Text;
            }
            string idColumnName = GetIdColumnNameByTableName(_tableName);
            string rowId = _selectedRow.ItemArray[0].ToString();
            string whereCondition = $"{idColumnName} = {rowId}";
            Debug.WriteLine($"idColumnName: {idColumnName}");
            Debug.WriteLine($"rowId: {rowId}");

            // Call UpdateRow method of DataAccessLayer to update the row
            dAL.UpdateLine(_tableName, rowId, _selectedRow, whereCondition);
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window without saving any changes
            this.DialogResult = false;
        }

        private string GetIdColumnNameByTableName(string tableName)
        {
            switch (tableName.ToLower())
            {
                case "bookdata":
                    return "ISBN";
                case "category":
                    return "CategoryID";
                case "discountdata":
                    return "Ccode";
                case "feedback":
                    return "FeedbackID";
                case "orderitem":
                    return "OrderID";
                case "orders":
                    return "OrderID";
                case "supplier":
                    return "SupplierId";
                case "userdata":
                    return "UserID";
                default:
                    return "ID";
            }
        }

    }
}

