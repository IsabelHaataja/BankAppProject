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

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BankAppDataContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<Func<BankAppDataContext>>(sp => () => sp.GetRequiredService<BankAppDataContext>());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
  .AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<BankAppDataContext>();

builder.Services.AddRazorPages();
builder.Services.AddTransient<DataInitializer>();
builder.Services.AddTransient<DataAccessService>();
//builder.Services.AddTransient<ICustomerService, CustomerService>();
//builder.Services.AddTransient<ICustomerDetails, CustomerDetailsService>();
//string flagBasePath = "/assets/img/flags";
//builder.Services.AddTransient<ICountryStatisticsService>(provider =>
//    new CountryStatisticsService(
//        provider.GetRequiredService<DataAccessService>(),
//        flagBasePath));
//builder.Services.AddTransient<IAccountService, AccountService>();
//builder.Services.AddTransient<IAccountDetailsService, AccountDetailsService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //scope.ServiceProvider.GetService<DataInitializer>().SeedData();
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

//app.UseResponseCaching();

app.Run();
