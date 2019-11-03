using AutoMapper;
using FCTAssignmentApp.Models;
using FCTAssignmentApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCTAssignmentApp.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, UserModel>();
            CreateMap<RegisterModel, Customer>();
        }
    }
}
