using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class CommentWithPageDto
    {
        // danh sach comment
        public IEnumerable<CommentDto> Comments { get; set; }
        // tong so comment
        public int TotalComment { get;set; }
    }
}
