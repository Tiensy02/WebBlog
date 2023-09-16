using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class CommentProfile : Profile
    {
        public CommentProfile() { 

            CreateMap<Comments,CommentInsertDto>();
            CreateMap<CommentInsertDto, Comments>();
            CreateMap<Comments, CommentUpdateDto>();

            CreateMap<CommentInsertForPostDto, Comments>();
            CreateMap<CommentInsertForPostDto, CommentInsertDto>();

            CreateMap<CommentModel, CommentInsertDto>();
            CreateMap<CommentModel, CommentInsertDto>();
            CreateMap<CommentModel, CommentDto>();

        }
    }
}
