using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class CommentUpdateDto
    {
        [Required]
        public Guid CommentID { get; set; }
        public string CommentContent { get; set; }
        [Required]
        public Guid UserID { get; set; }

    }
}
