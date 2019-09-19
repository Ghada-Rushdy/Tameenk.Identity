using AutoMapper;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.Company.Component
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap< AuthenticationLog , AuthenticationLogModel >();
            CreateMap< AuthenticationLogModel , AuthenticationLog >();
        }
    }
}
