using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        /// <summary>
        /// hàm thực hiện sửa trạng thái của nhiều thông báo
        /// </summary>
        /// <param name="ids"> danh sách id của thông báo</param>
        /// <returns></returns>
        Task UpdateNotificationStatus(List<Guid> ids);
        /// <summary>
        /// hàm thực hiện lấy danh sách thông báo của 1 user bằng id  người dùng
        /// </summary>
        /// <param name="userID"> id của người nhận thông báo</param>
        /// <returns></returns>
        Task<IEnumerable<Notification>> GetNotificationByUserID(Guid userID);

    }
}
