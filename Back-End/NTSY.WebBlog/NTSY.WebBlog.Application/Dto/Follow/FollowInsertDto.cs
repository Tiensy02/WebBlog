using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class FollowInsertDto
    {
        [Required]
        public Guid UserFollowID { get; set; }
        [Required]
        public Guid UserFollowed { get; set; }

    }
}
