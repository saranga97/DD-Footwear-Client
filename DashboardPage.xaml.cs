using System;
using System.Windows;

namespace WpfClient
{
    public partial class DashboardPage : Window
    {
        private readonly string _username;
        private readonly string _userType;

        public DashboardPage(string username, string userType)
        {
            InitializeComponent();
            _username = username;
            _userType = userType;

            // Set the username and userType in the TextBlock controls
            UsernameLabel.Text = $"Username: {_username}";
            UserTypeLabel.Text = $"User Type: {_userType}";
            DateTimeLabel.Text = $"Current Date and Time: {DateTime.Now}";
        }

        private void ViewProducts_Click(object sender, RoutedEventArgs e)
        {
            var viewProductsPage = new ViewProductsPage();
            viewProductsPage.Show();
        }

        private void ManageStock_Click(object sender, RoutedEventArgs e)
        {
            var manageStockPage = new ManageStockPage();
            manageStockPage.Show();
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            var placeOrderPage = new PlaceOrderPage();
            placeOrderPage.Show();
        }

        private void ViewOrders_Click(object sender, RoutedEventArgs e)
        {
            var viewOrdersPage = new ViewOrdersPage();
            viewOrdersPage.Show();
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Perform logout actions here, such as navigating back to the login page
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }
    }
}
