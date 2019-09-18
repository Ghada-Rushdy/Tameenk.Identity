using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.Individual.Component
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
