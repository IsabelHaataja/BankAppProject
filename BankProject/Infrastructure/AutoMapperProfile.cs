using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;
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
