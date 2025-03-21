using AutoMapper;
using finefin.api.Providers.Mapper;

namespace finefin_test._03._Builders.Providers
{
    public static class MapperBuilder
    {
        public static IMapper Build()
        {
            return new MapperConfiguration(options =>
            {
                options.AddProfile(new MappingConfig());
            }).CreateMapper();
        }
    }
}
