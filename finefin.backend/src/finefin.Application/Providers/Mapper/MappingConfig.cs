using AutoMapper;
using finefin.Domain.Entities;
using finefin.Shared.Communication.Requests.Transaction;
using finefin.Shared.Communication.Requests.User;
using finefin.Shared.Communication.Requests.Wallet;
using finefin.Shared.Communication.Responses.Transaction;

namespace finefin.Application.Providers.Mapper
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
                            opt => opt.MapFrom(src => src.Recurrence!.Type))
                .ForMember(dest => dest.Occurrences,
                            opt => opt.MapFrom(src => src.Recurrence != null ? src.Recurrence.Occurrences : 1)).ReverseMap();
        }
    }
}
