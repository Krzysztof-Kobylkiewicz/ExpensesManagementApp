using ExpensesManagementApp.Client;
using ExpensesManagementApp.Client.Services.TransactionService;
using ExpensesManagementApp.Client.Services.FileService;
using ExpensesManagementApp.Client.Services.StatisticsService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ExpensesManagementApp.Client.Services.CategoryService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddMudServices();

builder.Services.AddHttpClient("WebAPI", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();
