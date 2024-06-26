using Blazored.LocalStorage;
using FribergBlazorApp;
using FribergBlazorApp.Helpers;
using FribergBlazorApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7280/") });

//author: Christian Alp
builder.Services.AddScoped<AuthService>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
//author: Johan Kr�ngh
builder.Services.AddScoped<IOverlayService, OverlayService>();
//author: Pontus Lerman
builder.Services.AddScoped<HomeRedirect>();

await builder.Build().RunAsync();
