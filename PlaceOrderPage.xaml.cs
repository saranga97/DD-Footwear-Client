using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient
{
    public partial class PlaceOrderPage : Window
    {
        private readonly HttpClient _httpClient;

        public PlaceOrderPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            var productID = int.Parse(ProductIdTextBox.Text);
            var quantity = int.Parse(QuantityTextBox.Text);

            var order = new OrderModel
            {
                ProductID = productID,
                Quantity = quantity,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending"
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7258/api/Orders", order);
                response.EnsureSuccessStatusCode();
                MessageBox.Show("Order placed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class OrderModel
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
