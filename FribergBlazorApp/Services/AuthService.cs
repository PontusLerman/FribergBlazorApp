using System.Net.Http.Json;
using FribergWebAPI.DTOs;

namespace FribergBlazorApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private bool _isAuthenticated;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _isAuthenticated = false;
        }

        public bool IsAuthenticated => _isAuthenticated;

        public async Task<AuthResponseDto> Login(LoginRealtorDto model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Realtor/login", model);
                response.EnsureSuccessStatusCode();
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                _isAuthenticated = true;
                return authResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public async Task<AuthResponseDto> Register(RealtorDto model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Realtor/register", model);
                response.EnsureSuccessStatusCode();
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                _isAuthenticated = true;
                return authResponse;
            }
            catch (Exception ex)
            {
                // Handle error
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }
    }
}
