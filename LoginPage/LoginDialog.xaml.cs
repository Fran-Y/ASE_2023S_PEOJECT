using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using UnitTestLoginPage;

namespace BookStoreGUI
{
    /// <summary>
    /// LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        Window window;
        UserData userData;
        public LoginDialog()
        {
            InitializeComponent();
            userData = new UserData();

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (userData.LogIn(nameTextBox.Text, passwordTextBox.Password))
            {
                UserSession.CurrentUser = userData;  // Update the user session

                if (userData.IsManager)
                {
                    // If user is a manager, show the manager client window
                    window = new ManagerClient();  // Create the ManagerClientWindow
                }
                else
                {
                    // If user is not a manager, show the main window
                    window = new MainWindow();  // Create the MainWindow
                    ((MainWindow)window).statusTextBlock.Text = "You are logged in as User #" + userData.UserID;
                }

                this.Close();
                window.ShowDialog();
            }
            else
            {
                // If login failed, show an error message and do not close the dialog
                MessageBox.Show("Login failed. Please try again.");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
