using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class FollowsService : BaseService<FollowInsertDto, Follows, Follows>, IFollowsService
    {
        private readonly IFollowsRepository _followsRepository;
        public FollowsService(IFollowsRepository baseRepository, IMapper mapper, IFollowsRepository followsRepository) : base(baseRepository, mapper)
        {
            _followsRepository = followsRepository; 
        }

        public async Task DeleteFollowAsync(Guid followingID, Guid followedID)
        {
          await _followsRepository.DeleteFollowAsync(followingID, followedID);
        }

        /// <summary>
        /// hàm lấy danh sách người dùng đang follow user
        /// </summary>
        /// <param name="userFlollowID">id của người dùng được follow</param>
        /// <returns></returns>
        public async Task<(IEnumerable<FollowModel>,int)> GetUserFlollowed(Guid userFlollowID, Guid userCurrentID, int page, int pageSize)
        {
            var result = await _followsRepository.GetUserFlollowed(userFlollowID, userCurrentID, page,pageSize);
            return result;
        }
        /// <summary>
        /// hàm lấy danh sách người dùng  mà user đang follow
        /// </summary>
        /// <param name="userFollowingID"> id của người đi follow</param>
        /// <returns></returns>
        public async Task<(IEnumerable<FollowModel>, int)> GetUserFollowing(Guid userFollowingID, Guid userCurrentID, int page, int pageSize)
        {
            var result = await _followsRepository.GetUserFollowing(userFollowingID, userCurrentID, page, pageSize);
            return result;
        }
    }
}
