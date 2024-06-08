using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient
{
    public partial class ViewOrdersPage : Window
    {
        private readonly HttpClient _httpClient;

        public ViewOrdersPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            LoadOrders();
        }

        private async void LoadOrders()
        {
            try
            {
                var orders = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7258/api/Orders");
                OrdersDataGrid.ItemsSource = orders;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
