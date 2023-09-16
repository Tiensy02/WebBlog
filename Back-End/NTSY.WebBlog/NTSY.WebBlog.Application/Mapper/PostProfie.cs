using AutoMapper;
using NTSY.WebBlog.Application.Dto.Post;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class PostProfie : Profile
    {
        public PostProfie() {
            CreateMap<Posts, PostInsertDto>();
            CreateMap<Posts, PostUpdateDto>();
            CreateMap<PostInsertDto, Posts>();
            CreateMap<PostUpdateDto, Posts>();
            CreateMap<PostModel, PostInsertDto>();
            CreateMap<PostModel, PostUpdateDto>();
            CreateMap<PostModel, Posts>();
            CreateMap<Posts, PostModel>();

        }
    }
}
