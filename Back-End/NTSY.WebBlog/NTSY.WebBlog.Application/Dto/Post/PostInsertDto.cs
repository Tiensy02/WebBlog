using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application.Dto.Post
{
    public class PostInsertDto
    {
        [Required]
        public Guid UserID { get; set; }
        [Required]
        public string PostContent { get; set; }
        [Required]
        public string PostTitle { get; set; }
    }
}
