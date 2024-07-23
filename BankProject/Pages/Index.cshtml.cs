using DataAccessLayer.Models.CountrySystem;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace BankProject.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly ICountryStatisticsService _countryStatisticsService;
		public CountryStatisticsViewModel CountryStatistics { get; set; }

		public IndexModel(ILogger<IndexModel> logger, ICountryStatisticsService countryStatisticsService)
		{
			_logger = logger;
			_countryStatisticsService = countryStatisticsService;
		}

		public void OnGet()
		{
            //try
            //{
            //    CountryStatistics = _countryStatisticsService.GetStatisticsViewModel();

            //    if (CountryStatistics == null)
            //    {
            //        Console.WriteLine("CountryStatisticsViewModel is null. An error occurred in retrieving statistics.");
            //        // Initialize an empty view model to avoid null reference
            //        CountryStatistics = new CountryStatisticsViewModel();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"An error occurred while getting country statistics: {ex.Message}");
            //    // Handle the error accordingly
            //    CountryStatistics = new CountryStatisticsViewModel(); // Initialize an empty view model
            //}

        }
		//public string GetFlagUrl(Country country)
  //      {
  //          return _countryStatisticsService.GetFlagUrl(country);
  //      }
	}
}
