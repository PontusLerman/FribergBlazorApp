//Author: Pontus Lerman
using Blazored.LocalStorage;
using Blazored.LocalStorage.StorageOptions;
using FribergBlazorApp.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace FribergBlazorApp.Helpers
{
    public class HomeRedirect
    {
        private readonly NavigationManager navigationManager;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient http;

        public HomeRedirect(NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService, HttpClient http)
        {
            this.navigationManager = navigationManager;
            this.authenticationStateProvider = authenticationStateProvider;
            this.localStorageService = localStorageService;
            this.http = http;
        }

        //Use if you want to check if a SuperRealtor is logged in
        public void CheckIfSuperRealtor(ClaimsPrincipal user)
        {
            if (!user.IsInRole("SuperRealtor"))
            {
                navigationManager.NavigateTo("/");
            }
        }
        //Use if you want to check if either a DefaultRealtor or SuperRealtor is logged in
        public async Task CheckIfDefaultRealtorOrSuperRealtor(ClaimsPrincipal user)
        {
            var currentUser = await localStorageService.GetItemAsync<AuthResponseDto>("currentUser");
            var realtor = await http.GetFromJsonAsync<RealtorDto>($"api/realtor/{currentUser.Id}");
            if (!user.IsInRole("DefaultRealtor") && !user.IsInRole("SuperRealtor") || realtor.Approved == false)
            {
                    navigationManager.NavigateTo("/");
            }
        }
    }
}