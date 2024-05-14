using System.Net.Http.Json;
using Blazored.LocalStorage;
using FribergBlazorApp.DTOs;

namespace FribergBlazorApp.Services
{
	public class AuthService
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
		}

		public async Task<bool> IsAuthenticated()
		{
			return await _localStorage.ContainKeyAsync("authToken");
		}

		public async Task<AuthResponseDto> Login(LoginRealtorDto model)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("/api/Realtor/login", model);
				response.EnsureSuccessStatusCode();
				var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

				await _localStorage.SetItemAsync("authToken", authResponse.Token);
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
				
				await _localStorage.SetItemAsync("authToken", authResponse.Token);

				return authResponse;
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}");
			}
		}
		
		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("authToken");
		}
		
 		public async Task<RealtorDto> GetCurrentUser()
		{
            return await _localStorage.GetItemAsync<RealtorDto>("authToken");
		} 
	}
}
