using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public class Posts
    {
        public Guid PostID { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public Guid UserID { get; set; }    
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int PostRattingNumber { get; set; }
        public float PostRatingCore { get; set; }

    }
}
