using DataAccessLayer.Models;
using DataAccessLayer.Models.CountrySystem;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace DataAccessLayer.ViewModels
{
    public class CreateCustomerViewModel
    {
        [Range(1, 99, ErrorMessage = "Please choose a valid gender!")]
        public Gender GenderCustomer { get; set; }
        public List<SelectListItem> Genders { get; set; }

        public string Givenname { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Streetaddress { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Zipcode { get; set; } = null!;

        [Range(1, 99, ErrorMessage = "Please choose a valid country!")]
        public Country CountryCustomer { get; set; }
        public List<SelectListItem> Countries { get; set; }

        public DateOnly? Birthday { get; set; }

        public string? NationalId { get; set; }

        public string? Telephonecountrycode { get; set; }

        public string? Telephonenumber { get; set; }

        public string? Emailaddress { get; set; }
    }
}
