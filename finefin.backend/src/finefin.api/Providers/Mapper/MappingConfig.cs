using AutoMapper;
using finefin.api.Http.Requests;
using finefin.api.Models.Entities;

namespace finefin.api.Providers.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            RequestToEntity();
        }

        public void RequestToEntity()
        {
            CreateMap<RegisterUserRequest, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
