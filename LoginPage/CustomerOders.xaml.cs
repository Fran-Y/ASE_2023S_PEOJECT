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
using UnitTestLoginPage;

namespace BookStoreGUI
{
    /// <summary>
    /// CustomerOders.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerOders : Window
    {
        private DataAccessLayer DAL;
        public CustomerOders()
        {
            InitializeComponent();
            DAL = new DataAccessLayer(Properties.Settings.Default.ywConnectionString);
            LoadTableData();
        }

        private void LoadTableData()
        {
            DataTable dt = DAL.GetCustomerOrders(UserSession.CurrentUser.UserID);
            Console.WriteLine(UserSession.CurrentUser.UserID);
            TableDataGrid.ItemsSource = dt.DefaultView;
        }
    }

}
