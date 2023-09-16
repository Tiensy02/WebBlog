using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public class Users
    {
        //ID của người dùng
        public Guid UserID { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserAvatar { get; set; }
        public string UserDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

    }
}
