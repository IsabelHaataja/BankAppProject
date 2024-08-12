using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.CountrySystem
{
    public static class CountryExtension
    {
        public static string GetCountryCode(this Country country)
        {
            switch (country)
            {
                case Country.Sweden:
                    return "SE";
                case Country.Finland:
                    return "FI";
                case Country.Denmark:
                    return "DK";
                case Country.Norway:
                    return "NO";
                default:
                    throw new ArgumentException("Country code not found for the specified country.", nameof(country));
            }
        }
    }
}
