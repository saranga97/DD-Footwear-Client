using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace WpfClient
{
    public partial class LoginPage : Window
    {
        private readonly HttpClient _httpClient;

        public LoginPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var loginRequest = new
            {
                Username = username,
                Password = password
            };

            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://localhost:7258/api/auth/login", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                // Assuming the response contains a token or user details, handle accordingly
                // For example, navigate to the dashboard or store the token for future requests

                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Navigate to Dashboard and pass username and userType
                DashboardPage dashboard = new DashboardPage(username, "Your User Type Here");
                dashboard.Show();
                this.Close();
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Request error: {httpEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
