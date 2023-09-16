using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTSY.WebBlog.Domain;

namespace NTSY.WebBlog.Application
{
    public class NotificationInsertDto
    {
        [Required]
        public Guid UserID { get; set; }
        [Required]
        public string NotificationContent { get; set; }
        [Required]
        public NotificationType NotificationType { get; set; }

    }
}
