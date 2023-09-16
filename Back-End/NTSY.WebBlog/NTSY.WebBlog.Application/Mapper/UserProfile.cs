using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<Users, UserInsertDto>();
            CreateMap<Users, UserUpdateDto>();
            CreateMap<UserInsertDto,Users>();
            CreateMap<UserUpdateDto, Users>();
            CreateMap<UserLoginDto, Users>();
            CreateMap<Users, UserLoginDto>();


        }
    }
}
