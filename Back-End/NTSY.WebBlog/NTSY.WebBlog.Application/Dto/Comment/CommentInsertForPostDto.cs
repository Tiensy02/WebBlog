using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class CommentInsertForPostDto
    {
        public Guid CommentID { get; set; }
        public string CommentContent { get; set; }
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }
    }
}
