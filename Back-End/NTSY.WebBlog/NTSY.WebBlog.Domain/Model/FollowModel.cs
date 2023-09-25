using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public class FollowModel
    {
        // tên của người dùng
        public string UserName { get; set; }
        // id của bản ghi trong bảng follow
        public Guid FollowID { get; set; }
        // avatar của người dùng 
        public string UserAvatar { get; set; }
        // người dùng hiện thời follow hay không, 1 - có , 0 - không
        public int IsFollowedByUser { get; set; }
        public Guid UserID { get; set; }

    }
}
