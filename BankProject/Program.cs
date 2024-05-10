using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using BankProject.Infrastructure;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Autofac.Core.Registration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Use Autofac as the service provider
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
//{
//	containerBuilder.Populate(builder.Services);

//	containerBuilder.Register<Func<BankAppDataV2Context>>(c =>
//	{
//		var context = c.Resolve<IComponentContext>();
//		return () => context.Resolve<BankAppDataV2Context>();
//	}).As<Func<BankAppDataV2Context>>();

//	containerBuilder.RegisterType<DataAccessService>().AsSelf().InstancePerLifetimeScope();
//	containerBuilder.RegisterType<DataInitializer>().AsSelf().InstancePerLifetimeScope();
//	containerBuilder.Register(ctx =>
//	{
//		var config = new MapperConfiguration(cfg =>
//		{
//			cfg.AddProfile<AutoMapperProfile>();
//		});
//		return config.CreateMapper();
//	}).As<IMapper>().InstancePerLifetimeScope();
//	containerBuilder.RegisterType<CustomerService>().As<ICustomerService>();
//});


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
//builder.Services.AddTransient<ICustomerService, CustomerService>();
//builder.Services.AddTransient<IStatisticsService, StatisticsService>();
//builder.Services.AddTransient<ICustomerDetails, CustomerDetailService>();
//builder.Services.AddTransient<IAccountService, AccountService>();


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
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
