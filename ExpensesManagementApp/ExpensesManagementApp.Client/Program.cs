using ExpensesManagementApp.Client;
using ExpensesManagementApp.Client.Services;
using ExpensesManagementApp.Client.Services.FileService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

builder.Services.AddMudServices();

builder.Services.AddHttpClient("WebAPI", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();
