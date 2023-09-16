using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public interface INotificationService : IBaseService<NotificationInsertDto, Notification, Notification>
    {
        /// <summary>
        /// hàm thực hiện sửa trạng thái của nhiều thông báo
        /// </summary>
        /// <param name="ids"> danh sách id của thông báo</param>
        /// <returns></returns>
        Task UpdateNotificationStatus(List<Guid> ids);
        Task<IEnumerable<Notification>> GetNotificationByUserID(Guid userID);
    }
}
