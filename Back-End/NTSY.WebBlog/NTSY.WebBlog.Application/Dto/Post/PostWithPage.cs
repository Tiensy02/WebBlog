using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class PostWithPage
    {
        public IEnumerable<PostModel> Posts { get; set; }
        public int TotalPost { get; set; } 
    }
}
