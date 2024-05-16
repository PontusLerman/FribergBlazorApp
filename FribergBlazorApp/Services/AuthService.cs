using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using FribergBlazorApp.DTOs;
using Microsoft.AspNetCore.Components.Authorization;

namespace FribergBlazorApp.Services
{
	public class AuthService
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;
		private readonly AuthenticationStateProvider _authStateProvider;

		public AuthService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
			_authStateProvider = authStateProvider;
		}

		public async Task<bool> IsAuthenticated()
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");
			return !string.IsNullOrEmpty(token);
		}

		public async Task<AuthResponseDto> Login(LoginRealtorDto loginModel)
		{
			var response = await _httpClient.PostAsJsonAsync("/api/realtor/login", loginModel);
			if (response.IsSuccessStatusCode)
			{
				var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
				await _localStorage.SetItemAsync("authToken", authResponse.Token);
				await _localStorage.SetItemAsync("currentUser", authResponse);
				((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(authResponse.Token);
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
				return authResponse;
			}
			else
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"Login failed: {errorContent}");
			}
		}
		
		
		//public async Task<AuthResponseDto> Register(RealtorDto model)
		//{
		//	try
		//	{
		//		var response = await _httpClient.PostAsJsonAsync("api/Realtor/register", model);
		//		response.EnsureSuccessStatusCode();
		//		var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

		//		await _localStorage.SetItemAsync("authToken", authResponse.Token);

		//		return authResponse;
		//	}
		//	catch (Exception ex)
		//	{
		//		throw new Exception($"An error occurred: {ex.Message}");
		//	}
		//}
		
		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("authToken");
			((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}
		
		/* public async Task<RealtorDto> GetCurrentUser()
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");
			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				var response = await _httpClient.GetAsync("/api/realtor/current");
				if (response.IsSuccessStatusCode)
				{
					return await response.Content.ReadFromJsonAsync<RealtorDto>();
				}
			}
			throw new Exception("Failed to retrieve current user data.");
		} */
	}
}
