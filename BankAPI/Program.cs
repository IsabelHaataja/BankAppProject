
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BankAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddDbContext<BankAppDataV2Context>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddScoped<Func<BankAppDataV2Context>>(provider => () => provider.GetService<BankAppDataV2Context>());
			builder.Services.AddScoped<DataAccessService>();

			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			var logger = app.Services.GetRequiredService<ILogger<Program>>();
			logger.LogInformation("Using connection string: {ConnectionString}", builder.Configuration.GetConnectionString("DefaultConnection"));

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankApi v1");
					c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
				});
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
