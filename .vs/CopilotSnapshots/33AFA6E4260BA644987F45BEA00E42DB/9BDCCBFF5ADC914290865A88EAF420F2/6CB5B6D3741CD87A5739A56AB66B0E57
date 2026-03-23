using AutoMapper;
using BusinessLogic.Dtos;
using DataAccess.Models;

namespace BusinessLogic.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ═══════════════════════════════
            //  CUSTOMER
            // ═══════════════════════════════
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.AccountCount,
                           opt => opt.MapFrom(src => src.Accounts.Count));

            CreateMap<CreateCustomerDto, Customer>();

            CreateMap<UpdateCustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.NationalId, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Accounts, opt => opt.Ignore());

            // ═══════════════════════════════
            //  ACCOUNT
            // ═══════════════════════════════
            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.CustomerFullName,
                           opt => opt.MapFrom(src => src.Customer.FullName));

            CreateMap<CreateAccountDto, Account>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AccountNumber, opt => opt.Ignore())
                .ForMember(dest => dest.Balance, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.OpenedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ClosedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Transactions, opt => opt.Ignore());

            // ═══════════════════════════════
            //  TRANSACTION
            // ═══════════════════════════════
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.AccountNumber,
                           opt => opt.MapFrom(src => src.Account.AccountNumber));

            CreateMap<CreateTransactionDto, Transaction>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.BalanceAfter, opt => opt.Ignore())
                .ForMember(dest => dest.OccurredAt, opt => opt.Ignore())
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.RelatedAccount, opt => opt.Ignore());
        }
    }
}