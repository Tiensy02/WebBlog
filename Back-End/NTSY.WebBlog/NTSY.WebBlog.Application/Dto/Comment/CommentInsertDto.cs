using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class CommentInsertDto
    {
        public Guid CommentID { get; set; }
        public string CommentContent { get; set; }
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }
        public Guid CommentParentID { get; set; }

    }
}
