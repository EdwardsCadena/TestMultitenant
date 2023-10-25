using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Entities;

namespace TestProject.Infrastructure.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            CreateMap<Produc, ProductDTOs>();
            CreateMap<ProductDTOs, Produc>();

            CreateMap<User, UserDTOs>();
            CreateMap<UserDTOs, User>();

        }
    }
}
