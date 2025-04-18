using ExpensesManagementApp.Client.Services.TransactionService;
using ExpensesManagementApp.Client.Services.FileService;
using ExpensesManagementApp.Client.Services.StatisticsService;
using ExpensesManagementApp.Components;
using ExpensesManagementApp.Components.Account;
using ExpensesManagementApp.Logic.Repositories;
using ExpensesManagementApp.Logic.Repositories.FileRepository;
using ExpensesManagementApp.MapGroup;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ExpensesManagementApp.Logic.Repositories.TransactionsRepository;
using ExpensesManagementApp.Client.Services.CategoryService;
using ExpensesManagementApp.Logic.Repositories.CategoriesRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddScoped<IFileService, ExpensesManagementApp.Services.FileService.FileService>();
builder.Services.AddScoped<ITransactionService, ExpensesManagementApp.Services.TransactionService>();
builder.Services.AddScoped<IStatisticsService, ExpensesManagementApp.Services.StatisticsService>();
builder.Services.AddScoped<ICategoryService, ExpensesManagementApp.Services.CategoryService>();

builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<StatisticsRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ExpensesManagementApp.Database.ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ExpensesManagementApp.Database.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ExpensesManagementApp.Database.ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ExpensesManagementApp.Database.ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ExpensesManagementApp.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapGroup("").MapFiles();
app.MapGroup("").MapTransactions();
app.MapGroup("").MapStatistics();
app.MapGroup("").MapCategories();

app.Run();
