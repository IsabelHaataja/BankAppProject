using Microsoft.AspNetCore.Builder;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BankProject.Infrastructure;
using Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Ensure configuration is loaded before accessing it
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BankAppDataV2Context>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddScoped<Func<BankAppDataV2Context>>(sp => () => sp.GetRequiredService<BankAppDataV2Context>());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
  .AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<BankAppDataV2Context>();

builder.Services.AddRazorPages();
builder.Services.AddTransient<DataInitializer>();
builder.Services.AddTransient<DataAccessService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<ICustomerDetails, CustomerDetailsService>();
string flagBasePath = "/assets/img/flags";
builder.Services.AddTransient<ICountryStatisticsService>(provider =>
    new CountryStatisticsService(
        provider.GetRequiredService<DataAccessService>(),
        flagBasePath));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	scope.ServiceProvider.GetService<DataInitializer>().SeedData();

    var customerService = scope.ServiceProvider.GetService<ICustomerService>();
    //customerService.AssignCustomerNumbers();
    customerService.AssignAccountNumbers();
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
