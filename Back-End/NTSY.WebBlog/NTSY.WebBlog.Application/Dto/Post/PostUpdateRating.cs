using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class PostUpdateRating
    {
       public float RatingCore {  get; set; }
        public Guid PostID { get; set; }
    }
}
