using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;
using DataAccessLayer.Models.CountrySystem;
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
				.ForMember(dest => dest.Accounts, opt => opt.Ignore())
				.ForMember(dest => dest.Cards, opt => opt.Ignore())
				.ReverseMap();

			CreateMap<Account, AccountViewModel>().ReverseMap();

			CreateMap<Card, CardViewModel>().ReverseMap();

			CreateMap<Disposition, DispositionViewModel>().ReverseMap();
			CreateMap<Country, CountryStatisticsViewModel>().ReverseMap();

			CreateMap<Account, AccountDetailsViewModel>()
				.ForMember(dest => dest.Transactions, opt => opt.Ignore());

			CreateMap<Transaction, TransactionViewModel>()
				.ReverseMap();
		}
    }
}
