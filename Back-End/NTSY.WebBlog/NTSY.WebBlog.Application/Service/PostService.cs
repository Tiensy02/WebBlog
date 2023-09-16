using AutoMapper;
using NTSY.WebBlog.Application.Dto.Post;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{

    public class PostService : BaseService<PostInsertDto, PostUpdateDto, Posts>, IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository baseRepository, IMapper mapper, IPostRepository postRepository) : base(baseRepository, mapper)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostModel>> GetPostByFilter( string filter)
        {
            var result = await _postRepository.GetPostByFilter(filter);
            return result;
        }

        public async Task<PostModel> GetPostByID(Guid postID) 
        {
            var result = await _postRepository.GetPostByID(postID);
            return result;
        }

        public async Task<(IEnumerable<PostModel>, int)> GetPostForUI(int page, int pageSize)
        {
            var result = await _postRepository.GetPostForUI(page, pageSize);
            return result;
        }

        public async Task<(IEnumerable<PostModel>,int)> GetPostsOfUser(Guid userID, int page, int pageSize)
        {
            var result = await _postRepository.GetPostByUser(userID,page,pageSize);
            return result;
        }

        public async Task UpdateRating(float ratingCore, Guid postID)
        {
            await _postRepository.UpdateRating(ratingCore,postID);
        }
    }
}
