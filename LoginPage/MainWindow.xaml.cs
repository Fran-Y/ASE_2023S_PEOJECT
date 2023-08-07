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
        BookCatalog bookCat;
        LoginDialog loginDialog;
        
      
        public MainWindow() { 
            InitializeComponent(); 
            bookCat = new BookCatalog();
            bookOrder = new BookOrder();
            userData = new UserData();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            loginDialog = new LoginDialog();
            this.Close();
            loginDialog.ShowDialog();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BookCatalog bookCat = new BookCatalog();
            dsBookCat = bookCat.GetBookInfo();
            this.DataContext = dsBookCat.Tables["Category"];
            
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
            DataRowView selectedRow;
            selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
            string isbn = selectedRow.Row.ItemArray[0].ToString();
          
            FeedbackDialog feedbackItemDialog = new FeedbackDialog(isbn);
            feedbackItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
            
            feedbackItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
            feedbackItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
            feedbackItemDialog.Owner = this;
            feedbackItemDialog.ShowDialog();
            if (feedbackItemDialog.DialogResult == true)
            {
                //string isbn = feedbackItemDialog.isbnTextBox.Text;
                string title = feedbackItemDialog.titleTextBox.Text;
                double unitPrice = double.Parse(feedbackItemDialog.priceTextBox.Text);
                //string feedback = int.Parse(feedbackItemDialog.quantityTextBox.Text);
                //bookOrder.AddItem(new OrderItem(isbn, title, unitPrice, feedback));
            }
        }

        public void detailButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItems.Count > 0)
            {
                DetailDialog detailDialog = new DetailDialog();
                DataRowView selectedRow;
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                string isbn = selectedRow.Row.ItemArray[0].ToString();
                detailDialog.isbnTextBox.Text = isbn;
                detailDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
                detailDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
                detailDialog.authorTextBox.Text = selectedRow.Row.ItemArray[3].ToString();
                detailDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
                detailDialog.yearTextBox.Text = selectedRow.Row.ItemArray[5].ToString();
                DALFeedback dalFeedback = new DALFeedback();
                string feedback = dalFeedback.GetFeedback(isbn);
                detailDialog.feedbackTextBox.Text = feedback != null ? feedback : "No feedback found";

                detailDialog.ShowDialog();
                if (detailDialog.DialogResult == true)
                {
                    isbn = detailDialog.isbnTextBox.Text;
                    string title = detailDialog.titleTextBox.Text;
                    string author = detailDialog.authorTextBox.Text;
                    double unitPrice = double.Parse(detailDialog.priceTextBox.Text);
                    string year = detailDialog.yearTextBox.Text;
                    feedback = detailDialog.feedbackTextBox.Text;
              
                }
            }
            else MessageBox.Show("Please select one book from above.");
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
            int userId = UserSession.CurrentUser.UserID;  // Get the user ID from the session
            if (userId > 0)
            {
                orderId = bookOrder.PlaceOrder(userId);
                MessageBox.Show("Your order has been placed. Your order id is " +
                orderId.ToString());
            }
            else
            {
                MessageBox.Show("Please log in before placing an order.");
            }
        }


        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchText = searchTextBox.Text;
            var searchResults = bookCat.SearchBooks(searchText); 
            ProductsDataGrid.ItemsSource = searchResults.Tables["Books"].DefaultView;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox.Text = "";  // Clear the search text box
            var allBooks = bookCat.GetBookInfo();  // Get all books
            ProductsDataGrid.ItemsSource = allBooks.Tables["Books"].DefaultView;  // Refresh the data grid
        }

       

    }
}
