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
        MainWindow mainWindow;
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
                mainWindow = new MainWindow();  // Create the MainWindow
                mainWindow.statusTextBlock.Text = "You are logged in as User #" +
                    userData.UserID;
                this.Close();
                mainWindow.ShowDialog();
                // If user login successfully, show the main window
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
