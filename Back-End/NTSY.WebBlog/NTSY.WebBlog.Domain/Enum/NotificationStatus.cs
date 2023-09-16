using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    // enum cho trạng thái của thông báo, đã được đọc hay chưa
    public enum NotificationStatus
    {
        IsRead = 1 , // đã đọc
        NotRead = 0, // chưa đọc
    }
}
