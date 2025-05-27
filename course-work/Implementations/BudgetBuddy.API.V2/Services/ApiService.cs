    using BudgetBuddy.API.V2.Models;

    namespace BudgetBuddy.API.V2.Services
    {
        public class ApiService
        {
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
            {
                _httpClient = httpClient;
                _httpContextAccessor = httpContextAccessor;

                var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
            }
        }
    }
