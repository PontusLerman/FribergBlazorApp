//Author: Pontus Lerman
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace FribergBlazorApp.Helpers
{
    public class HomeRedirect
    {
        private readonly NavigationManager navigationManager;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public HomeRedirect(NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
        {
            this.navigationManager = navigationManager;
            this.authenticationStateProvider = authenticationStateProvider;
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
        public void CheckIfDefaultRealtorOrSuperRealtor(ClaimsPrincipal user)
        {
            if (!user.IsInRole("DefaultRealtor") && !user.IsInRole("SuperRealtor"))
            {
                navigationManager.NavigateTo("/");
            }
        }
    }
}