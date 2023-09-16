using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public class Notification
    {
        public Guid NotificationID { get; set; }
        public Guid UserID { get; set; }
        public string NotificationContent { get; set; }
        public int NotificationType { get; set; }
        public int NotificationStatus { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
