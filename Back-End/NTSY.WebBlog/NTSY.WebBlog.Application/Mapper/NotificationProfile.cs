using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile() {
            CreateMap<Notification, NotificationInsertDto>();
            CreateMap<NotificationModel,NotificationInsertDto>();
            CreateMap<NotificationInsertDto, Notification>();
        }
    }
}
