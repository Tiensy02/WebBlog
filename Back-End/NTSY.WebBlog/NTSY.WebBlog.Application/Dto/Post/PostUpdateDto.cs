using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application.Dto.Post
{
    public class PostUpdateDto
    {
        [Required]
        public Guid PostID { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }

    }
}
