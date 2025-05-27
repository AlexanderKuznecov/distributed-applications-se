using BudgetBuddy.Web.V3.Models;
using Microsoft.AspNetCore.Identity.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BudgetBuddy.Web.V3.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("API");
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddToken()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var loginModel = new { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

            if (!response.IsSuccessStatusCode)
                return false;

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                return false;

            _httpContextAccessor.HttpContext?.Session.SetString("JWToken", loginResponse.Token);
            return true;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode;
        }


        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            AddToken();
            var response = await _httpClient.GetAsync("api/categories");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>()
                : new List<CategoryViewModel>();
        }

        public async Task<CategoryViewModel?> GetCategoryByIdAsync(int id)
        {
            AddToken();
            var response = await _httpClient.GetAsync($"api/categories/{id}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<CategoryViewModel>()
                : null;
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryViewModel model)
        {
            AddToken();
            var response = await _httpClient.PostAsJsonAsync("api/categories", model);
            return response.IsSuccessStatusCode;
        }



        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryViewModel model)
        {
            AddToken();
            var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            AddToken();
            var response = await _httpClient.DeleteAsync($"api/categories/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<TransactionViewModel>> GetTransactionsAsync()
        {
            AddToken();
            var response = await _httpClient.GetAsync("api/transactions");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<List<TransactionViewModel>>() ?? new()
                : new();
        }

        public async Task<TransactionViewModel?> GetTransactionByIdAsync(int id)
        {
            AddToken();
            var response = await _httpClient.GetAsync($"api/transactions/{id}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<TransactionViewModel>()
                : null;
        }

        public async Task<bool> CreateTransactionAsync(CreateTransactionViewModel model)
        {
            AddToken();
            var response = await _httpClient.PostAsJsonAsync("api/transactions", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTransactionAsync(int id, UpdateTransactionViewModel model)
        {
            AddToken();
            var response = await _httpClient.PutAsJsonAsync($"api/transactions/{id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            AddToken();
            var response = await _httpClient.DeleteAsync($"api/transactions/{id}");
            return response.IsSuccessStatusCode;
        }

    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
