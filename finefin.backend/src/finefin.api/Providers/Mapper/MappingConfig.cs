using AutoMapper;
using finefin.api.Http.Requests;
using finefin.api.Http.Responses;
using finefin.api.Models.Entities;

namespace finefin.api.Providers.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            RequestToEntity();
            EntityToResponse();
        }

        public void RequestToEntity()
        {
            CreateMap<RegisterUserRequest, LocalUser>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<CreateWalletRequest, Wallet>();
            CreateMap<CreateTransactionRequest, Transaction>();
            CreateMap<RecurrenceRequest, Recurrence>();
        }

        public void EntityToResponse()
        {
            CreateMap<Transaction, PendingTransactionResponse>()
                .ForMember(dest => dest.RecurrenceType,
                            opt => opt.MapFrom(src => src.Recurrence != null ? src.Recurrence.Type : string.Empty))
                .ForMember(dest => dest.Occurrences,
                            opt => opt.MapFrom(src => src.Recurrence != null ? src.Recurrence.Occurrences : 1)).ReverseMap();
        }
    }
}
