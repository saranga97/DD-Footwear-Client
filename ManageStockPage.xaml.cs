using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient
{
    public partial class ManageStockPage : Window
    {
        private readonly HttpClient _httpClient;

        public ManageStockPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            LoadStock();
        }

        private async void LoadStock()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<ProductModel>>("https://localhost:7258/api/Products");
                StockDataGrid.ItemsSource = products;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateStock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var products = (List<ProductModel>)StockDataGrid.ItemsSource;
                foreach (var product in products)
                {
                    await _httpClient.PutAsJsonAsync($"https://localhost:7258/api/Products/{product.ProductID}", product);
                }
                MessageBox.Show("Stock updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int StockLevel { get; set; }
        public decimal Price { get; set; }
    }
}
