using DataAccessLayer;
using DataAccessLayer.Models.CountrySystem;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryStatisticsService /*: ICountryStatisticsService*/
    {
        //private readonly DataAccessService _dataAccessService;
        //private readonly string _flagBasePath;
        //public CountryStatisticsService (DataAccessService dataAccessService, string flagBasePath)
        //{
        //    _dataAccessService = dataAccessService;
        //    _flagBasePath = flagBasePath;
        //}

        //public string GetFlagUrl(Country country)
        //{

        //    return $"{_flagBasePath}/{country.ToString().ToLower()}.svg";
        //}
    
        //public CountryStatisticsViewModel GetStatisticsViewModel()
        //{
        //    try
        //    {
        //        var viewModel = new CountryStatisticsViewModel();
        //        var countries = Enum.GetValues(typeof(Country)).Cast<Country>().Skip(1);

        //        foreach (var country in countries)
        //        {
        //            var customerIds = _dataAccessService.GetCustomerIdByCountry(country);
        //            viewModel.NumberOfCustomers[country] = customerIds.Count();
        //            viewModel.NumberOfAccounts[country] = _dataAccessService.GetAccountCountByCountry(country);
        //            viewModel.TotalBalance[country] = _dataAccessService.GetTotalBalanceByCountry(customerIds);
        //        }

        //        return viewModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception in GetStatisticsViewModel: " + ex.Message);
        //        return new CountryStatisticsViewModel(); ;
        //    }
        //}
    }
}
