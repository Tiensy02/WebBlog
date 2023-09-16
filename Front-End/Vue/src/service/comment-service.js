import BaseService from "./base-service";
class CommentService extends BaseService{
    constructor(){
        super("https://localhost:7070/api/Comment")
    }
    /**
     * @description hàm thực hiện lấy những comment gốc ( comment không có comment cha)
     * @param {Guid} postID id của bài viết
     * @param {int} page trang bình luận hiện tại
     * @param {int} pageSize tổng số bình luận trên 1 khung bình luận
     * @returns 
     */
    async getCommentByPostId(postID, page, pageSize ){
        const url = this.endpoint(`/${postID}/comment-post`)
        const params = {postID,page,pageSize}
        const res = await this.baseAxios.get(url,{params})
        return res.data
    }
    /**
     * @description hàm thực hiện lấy tất cả comment con của 1 commnet
     * @param {Guid} commentID id của comment
     * @returns 
     */
    async getCommentChild(commentID){
        const url = this.endpoint(`/${commentID}/comment-child`)
        const params = {commentID}
        const res = await this.baseAxios.get(url,params)
        return res.data
    }
    async postCommetRoot(comment){
        const url = "https://localhost:7070/CommentRoot"
        const res = await this.baseAxios.post(url,comment)
        return res.data
    }
}
export default CommentService