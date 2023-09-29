using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public interface IFollowsService : IBaseService<FollowInsertDto,Follows,Follows>
    {
        /// <summary>
        /// hàm lấy danh sách người dùng đang flollow user có id là userFlollowID
        /// </summary>
        /// <param name="userFlollowID"> id của người dùng được flollow</param>
        /// <returns></returns>
        Task<(IEnumerable<FollowModel>, int)> GetUserFlollowed(Guid userFlollowID,Guid userCurrentID, int page, int pageSize);
        /// <summary>
        /// hàm lấy danh sách người dùng mà user có id là userFlollowID đang flollow
        /// </summary>
        /// <param name="userFollowingID"></param>
        /// <returns></returns>
        Task<(IEnumerable<FollowModel>, int)> GetUserFollowing(Guid userFollowingID,Guid userCurrentID, int page, int pageSize);
        /// <summary>
        /// ham thực hiện xoá 1 luồng follow
        /// </summary>
        /// <param name="followingID">id của người đi theo dõi  </param>
        /// <param name="followedID">id của người được theo dõi</param>
        /// <returns></returns>
        Task DeleteFollowAsync(Guid followingID, Guid followedID);
    }
}
