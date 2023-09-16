using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class FollowWithPage
    {
        public IEnumerable<FollowModel> Follows { get; set; }
        public int TotalFollow {  get; set; }
    }
}
