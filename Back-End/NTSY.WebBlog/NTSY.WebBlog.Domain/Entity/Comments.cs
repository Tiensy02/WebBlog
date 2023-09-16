using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public class Comments
    {
        public Guid CommentID { get; set; }
        public string CommentContent { get; set; }
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }
        public Guid CommentParentID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
