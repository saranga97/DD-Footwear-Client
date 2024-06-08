using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient
{
    public partial class ViewProductsPage : Window
    {
        private readonly HttpClient _httpClient;

        public ViewProductsPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7258/api/Products");
                ProductsDataGrid.ItemsSource = products;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int StockLevel { get; set; }
        public decimal Price { get; set; }
    }
}
