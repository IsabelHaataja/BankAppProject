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

			CreateMap<Customer, CustomerViewModel>()
	            .ForMember(dest => dest.Dispositions, opt => opt.MapFrom(src => src.Dispositions))
	            .ForMember(dest => dest.Accounts, opt => opt.Ignore()) // Assuming manual handling or separate mapping
	            .ForMember(dest => dest.Cards, opt => opt.Ignore()) // Assuming manual handling or separate mapping
	            .ReverseMap();

			CreateMap<Account, AccountViewModel>().ReverseMap();

			CreateMap<Card, CardViewModel>().ReverseMap();

			CreateMap<Disposition, DispositionViewModel>().ReverseMap();
		}
    }
}
