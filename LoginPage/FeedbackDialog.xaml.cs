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

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for FeedbackDialog.xaml
    /// </summary>
    public partial class FeedbackDialog : Window
    {
        public FeedbackDialog()
        {
            InitializeComponent();

            viewModel = new FeedbackDialogViewModel();
            this.DataContext = viewModel;
        }

        private void quantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private FeedbackDialogViewModel viewModel;

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SubmitFeedback())
            {
                MessageBox.Show("Thank you for your feedback.");
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("No valid feedback.");
                this.DialogResult = false;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
