using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public class Follows
    {
        public Guid FollowID { get; set; }
        // id của người đi theo dõi
        public Guid UserFollowID { get; set; }
        // id của người được theo dõi
        public Guid UserFollowed { get; set; }
    }
}
