using AutoMapper;
using KD_API.Models;
using KD_API.Models.APIRequests.Cattle;
using KD_API.Models.APIRequests.CreditNote;
using KD_API.Models.APIRequests.Customer;
using KD_API.Models.APIRequests.Product;
using KD_API.Models.APIResponse.Cattle;
using KD_API.Models.APIResponse.CreditNote;
using KD_API.Models.APIResponse.Customer;
using KD_API.Models.APIResponse.Expense;
using KD_API.Models.APIResponse.Price;
using KD_API.Models.APIResponse.Product;
using KD_API.Models.APIResponse.Transaction;

namespace KD_API.Models.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        #region Cattle mappings

        CreateMap<CattleDTO, CattleResponse>();

        CreateMap<IEnumerable<CattleDTO>, CattleListResponse>()
            .ForMember(dest => dest.Cattle, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.Count()))
            .ForMember(dest => dest.ActiveCount, opt => opt.MapFrom(src => src.Count(c => c.IsActive)))
            .ForMember(dest => dest.InactiveCount, opt => opt.MapFrom(src => src.Count(c => !c.IsActive)));

        // Request to DTO mappings
        CreateMap<CreateCattleRequest, CattleDTO>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UpdateCattleRequest, CattleDTO>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        #endregion Cattle mappings

        #region CreditNote mappings
        CreateMap<CreateCreditNoteRequest, CreditNoteDTO>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UpdateCreditNoteRequest, CreditNoteDTO>();
        CreateMap<CreditNoteDTO, CreditNoteResponse>();
        CreateMap<IEnumerable<CreditNoteDTO>, CreditNoteListResponse>()
            .ForMember(dest => dest.CreditNotes, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.Count()))
            .ForMember(dest => dest.TotalAmount, opt => 
                opt.MapFrom(src => src.Sum(c => c.TotalAmount)))
            .ForMember(dest => dest.PaidAmount, opt => 
                opt.MapFrom(src => src.Where(c => c.Status == Models.Enums.CreditStatus.Paid).Sum(c => c.TotalAmount)))
            .ForMember(dest => dest.PendingAmount, opt => 
                opt.MapFrom(src => src.Where(c => c.Status == Models.Enums.CreditStatus.Unpaid || c.Status == Models.Enums.CreditStatus.PartiallyPaid).Sum(c => c.TotalAmount)))
            .ForMember(dest => dest.StatusCounts, opt => opt.MapFrom(src => 
                src.GroupBy(c => c.Status.ToString())
                    .ToDictionary(g => g.Key, g => g.Count())))
            .ForMember(dest => dest.OverdueCount, opt => opt.MapFrom(src => 
                src.Count(c => c.DueDate < DateTime.UtcNow && c.Status != Models.Enums.CreditStatus.Paid)));
        #endregion CreditNote mappings

        #region Customer mappings
        // Customer mappings
        CreateMap<CreateCustomerRequest, CustomerDTO>();
        CreateMap<UpdateCustomerRequest, CustomerDTO>();
        CreateMap<CustomerDTO, CustomerResponse>();
        #endregion

        #region Poduct mappings
        CreateMap<CreateProductRequest, ProductDTO>();
        CreateMap<UpdateProductRequest, ProductDTO>();
        CreateMap<ProductDTO, ProductResponse>();
        #endregion

        // Transaction mappings
        CreateMap<TransactionDTO, TransactionResponse>();

        // Price mappings
        CreateMap<PriceDTO, PriceResponse>();

        // Expense mappings
        CreateMap<ExpenseDTO, ExpenseResponse>();
    }
}
