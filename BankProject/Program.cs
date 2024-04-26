using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<BankAppDataV2Context>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<BankAppDataV2Context>();
builder.Services.AddRazorPages();
builder.Services.AddTransient<DataInitializer>();

var containerBuilder = new ContainerBuilder();

containerBuilder.RegisterType<BankAppDataV2Context>().AsSelf().InstancePerLifetimeScope();

containerBuilder.RegisterType<DataAccessService>().AsSelf().InstancePerLifetimeScope();

containerBuilder.Populate(builder.Services);
var container = containerBuilder.Build();

builder.Services.AddSingleton<IServiceProvider>(container.Resolve<IServiceProvider>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	scope.ServiceProvider.GetService<DataInitializer>().SeedData();
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
