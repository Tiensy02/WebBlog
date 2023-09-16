using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public interface IFollowsRepository : IBaseRepository<Follows>
    {
        /// <summary>
        /// hàm lấy danh sách người dùng đang flollow user có id là userFlollowID
        /// </summary>
        /// <param name="userFlollowID"> id của người dùng được flollow</param>
        /// <returns></returns>
        Task<(IEnumerable<FollowModel>,int)> GetUserFlollowed(Guid userFlollowID,int page , int pageSize );
        /// <summary>
        /// hàm lấy danh sách người dùng mà user có id là userFlollowID đang flollow
        /// </summary>
        /// <param name="userFollowingID"></param>
        /// <returns></returns>
        Task<(IEnumerable<FollowModel>,int)> GetUserFollowing(Guid userFollowingID, int page, int pageSize);
    }
}
