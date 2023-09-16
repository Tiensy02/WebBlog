import BaseService from "./base-service";
class PostService extends BaseService{
    constructor(){
        super("https://localhost:7070/api/Post")
    }
    /**
     * @description hàm thực hiện lấy các bài viết theo phân trang
     * @param {int} page số trang hiện tại
     * @param {int } pageSize số bản ghi trên 1 trang
     * @returns 
     */
    async getPostListForUI(page, pageSize) {
        const url = this.endpoint("/PostList")
        const params = {page,pageSize}
        const res = await this.baseAxios.get(url,{params})
        return res.data
    }
    async getPostById(postID) {
        const url = this.endpoint(`/${postID}/post-content`)
        const params = {postID}
        const res = await this.baseAxios.get(url,{params})
        return res.data
    }
    async getPostByFilter(filter) {
        const url = this.endpoint("/Filter")
        const params = {filter}
            const res = await this.baseAxios.get(url,{params})
            return res.data
    }
    /**
     * @description hàm cập nhập điểm đánh giá của bài viết
     * @param {double} ratingCore điểm đánh giá
     * @param {guid } postID id của bài viết
     * @returns 
     */
    async updatePostRating(ratingCore, postID) {
        const url = this.endpoint("/RatingCore")
        const postUpdateRating = {ratingCore,postID}
        const res = await this.baseAxios.put(url,postUpdateRating)
        return res
    }
    /**
     * @description hàm thực hiện lấy danh sách bài viết của người dùng cụ thể
     * @param {GUID} userID id của người dùng
     * @returns 
     */
    async getPostOfUser(userID){
        const url = this.endpoint(`/User/${userID}`)
        const params = {userID}
        const res = await this.baseAxios.get(url, params)
        return res.data
    }
}
export default PostService