using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public class PostModel
    {
        public Guid PostID { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public Guid UserID { get; set; }
        public float PostRatingCore { get; set; }
        public int PostRattingNumber { get; set; }
        public int NumberCommentOfPost { get; set; }
    }
}
