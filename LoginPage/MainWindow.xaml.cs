/* ************************************************************
 * For students to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * ************************************************************/
using System;
using System.Collections.Generic;
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
using System.Data;
using BookStoreLIB;
using System.Collections.ObjectModel;
using UnitTestLoginPage;

namespace BookStoreGUI
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        DataSet dsBookCat;
        UserData userData;
        BookOrder bookOrder;
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog dlg = new LoginDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            // Process data entered by user if dialog box is accepted
            if (dlg.DialogResult == true)
            {
                if (userData.LogIn(dlg.nameTextBox.Text, dlg.passwordTextBox.Password) == true)
                    this.statusTextBlock.Text = "You are logged in as User #" +
                    userData.UserID;
                else
                    this.statusTextBlock.Text = "Your login failed. Please try again.";
            }
        }
        private void exitButton_Click(object sender, RoutedEventArgs e) { this.Close(); }
        public MainWindow() { InitializeComponent(); }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BookCatalog bookCat = new BookCatalog();
            dsBookCat = bookCat.GetBookInfo();
            this.DataContext = dsBookCat.Tables["Category"];
            bookOrder = new BookOrder();
            userData = new UserData();
            this.orderListView.ItemsSource = bookOrder.OrderItemList;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OrderItemDialog orderItemDialog = new OrderItemDialog();
            DataRowView selectedRow;
            selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
            orderItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
            orderItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
            orderItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
            orderItemDialog.Owner = this;
            orderItemDialog.ShowDialog();
            if (orderItemDialog.DialogResult == true)
            {
                string isbn = orderItemDialog.isbnTextBox.Text;
                string title = orderItemDialog.titleTextBox.Text;
                double unitPrice = double.Parse(orderItemDialog.priceTextBox.Text);
                int quantity = int.Parse(orderItemDialog.quantityTextBox.Text);
                bookOrder.AddItem(new OrderItem(isbn, title, unitPrice, quantity));
            }
        }
        private void rateButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackDialog feedbackItemDialog = new FeedbackDialog();
            DataRowView selectedRow;
            selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
            feedbackItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
            feedbackItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
            feedbackItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
            feedbackItemDialog.Owner = this;
            feedbackItemDialog.ShowDialog();
            if (feedbackItemDialog.DialogResult == true)
            {
                string isbn = feedbackItemDialog.isbnTextBox.Text;
                string title = feedbackItemDialog.titleTextBox.Text;
                double unitPrice = double.Parse(feedbackItemDialog.priceTextBox.Text);
                //string feedback = int.Parse(feedbackItemDialog.quantityTextBox.Text);
                //bookOrder.AddItem(new OrderItem(isbn, title, unitPrice, feedback));
            }
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderListView.SelectedItem != null)
            {
                var selectedOrderItem = this.orderListView.SelectedItem as OrderItem;
                bookOrder.RemoveItem(selectedOrderItem.BookID);

                // Refresh the ListView after removing the book
                this.orderListView.ItemsSource = null;
                this.orderListView.ItemsSource = bookOrder.OrderItemList;
            }
        }
        private void chechoutButton_Click(object sender, RoutedEventArgs e)
        {
            int orderId;
            orderId = bookOrder.PlaceOrder(userData.UserID);
            MessageBox.Show("Your order has been placed. Your order id is " +
            orderId.ToString());
        }


    }
}
