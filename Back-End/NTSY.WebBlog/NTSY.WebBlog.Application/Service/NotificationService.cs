using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class NotificationService : BaseService<NotificationInsertDto, Notification, Notification>, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            _notificationRepository = baseRepository;
        }

        public async Task<IEnumerable<Notification>> GetNotificationByUserID(Guid userID)
        {
            var result = await _notificationRepository.GetNotificationByUserID(userID);
            return result;
        }

        public async Task UpdateNotificationStatus(List<Guid> ids)
        {
            await _notificationRepository.UpdateNotificationStatus(ids); 
        }
    }
}
