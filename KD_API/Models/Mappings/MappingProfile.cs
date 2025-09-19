using AutoMapper;
using KD_API.Models;
using KD_API.Models.APIRequests.Cattle;
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
        // Cattle mappings
        CreateMap<CattleDTO, CattleResponse>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => (DateTime?)null));

        CreateMap<IEnumerable<CattleDTO>, CattleListResponse>()
            .ForMember(dest => dest.Cattle, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.Count()))
            .ForMember(dest => dest.ActiveCount, opt => opt.MapFrom(src => src.Count(c => c.IsActive)))
            .ForMember(dest => dest.InactiveCount, opt => opt.MapFrom(src => src.Count(c => !c.IsActive)));

        // Request to DTO mappings
        CreateMap<CreateCattle, CattleDTO>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateCattle, CattleDTO>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

        // Customer mappings
        CreateMap<CustomerDTO, CustomerResponse>();

        // Product mappings
        CreateMap<ProductDTO, ProductResponse>();

        // Transaction mappings
        CreateMap<TransactionDTO, TransactionResponse>();

        // Price mappings
        CreateMap<PriceDTO, PriceResponse>();

        // CreditNote mappings
        CreateMap<CreditNoteDTO, CreditNoteResponse>();

        // Expense mappings
        CreateMap<ExpenseDTO, ExpenseResponse>();
    }
}
