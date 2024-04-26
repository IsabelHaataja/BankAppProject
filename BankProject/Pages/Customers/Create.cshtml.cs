using DataAccessLayer.Models.CountrySystem;
using DataAccessLayer.Models;
using BankProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using AutoMapper;

namespace BankProject.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CreateCustomerViewModel CreateCustomerRequest { get; set; }
        public CreateModel(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
            CreateCustomerRequest = new CreateCustomerViewModel();
        }
        private void FillGenderList()
        {
            CreateCustomerRequest.Genders = Enum.GetValues<Gender>()
                .Select(g => new SelectListItem
                {
                    Value = g.ToString(),
                    Text = g.ToString()
                }).ToList();
        }
        private void FillCountryList()
        {
            CreateCustomerRequest.Countries = Enum.GetValues<Country>()
                .Select(g => new SelectListItem
                {
                    Value = g.ToString(),
                    Text = g.ToString()
                }).ToList();
        }
        public void OnGet()
        {
            FillCountryList();
            FillGenderList();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                _mapper.Map(CreateCustomerRequest, customer);

                _customerService.SaveNew(customer);

                ViewData["Message"] = "Employee created successfully!";

                return RedirectToPage("Index");
            }

            FillCountryList();
            FillGenderList();
            return Page();
        }
    }
}
