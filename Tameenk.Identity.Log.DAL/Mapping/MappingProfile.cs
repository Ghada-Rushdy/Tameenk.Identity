using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tameenk.Identity.Log.DAL.Mapping
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
