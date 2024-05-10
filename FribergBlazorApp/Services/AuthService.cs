using System.Net.Http.Json;
using FribergWebAPI.DTOs;

namespace FribergBlazorApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponseDto> Login(LoginRealtorDto model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Realtor/login", model);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
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
                return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            }
            catch (Exception ex)
            {
                // Handle error
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }
    }
}
