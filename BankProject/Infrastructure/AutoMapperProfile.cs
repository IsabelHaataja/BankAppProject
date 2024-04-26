using AutoMapper;
using DataAccessLayer.Models;
using BankProject.ViewModels;
namespace BankProject.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerSearchViewModel, Customer>()
                .ReverseMap();
            CreateMap<CreateCustomerViewModel, Customer>() 
                .ReverseMap();
        }
    }
}
