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

        public void CheckIfSuperRealtor(ClaimsPrincipal user)
        {
            if (!user.IsInRole("SuperRealtor"))
            {
                navigationManager.NavigateTo("/");
            }
        }
        public void CheckIfDefaultRealtorOrSuperRealtor(ClaimsPrincipal user)
        {
            if (!user.IsInRole("DefaultRealtor") && !user.IsInRole("SuperRealtor"))
            {
                navigationManager.NavigateTo("/");
            }
        }
    }
}
