using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class FollowProfile : Profile
    {
        public FollowProfile()
        {
            CreateMap<Follows,FollowInsertDto>();
            CreateMap<FollowModel,FollowInsertDto>();
        }
    }
}
