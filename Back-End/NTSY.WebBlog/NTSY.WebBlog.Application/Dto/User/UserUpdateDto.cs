using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class UserUpdateDto
    {
        [Required]
        public Guid UserID { get; set; }
        public string UserPassword { get; set; }
        public string UserAvatar { get; set; }
        public string UserDescription { get; set; }
     }
}
