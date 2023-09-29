<template>
    <div class="comment-list-wrap">
        <p class="comment-title">Comments</p>
        <comment-write :userID="userID" :propPostID="postID" typeComment="post" @addCommentPostSucess="addCommentPostSucess($event)"></comment-write>
        <CommentRoot  @deleteSuccess="deleteSuccess($event)" :propPostID="postID" :propCommentRoots="comments"></CommentRoot>
    </div>
</template>
<script>
import CommentService from '../../service/comment-service.js'
import CommentRoot from './CommentRoot.vue';
export default {
    name:"wb-comment",
    components:{
        CommentRoot
    },
    created(){
    },
    data(){
        return { 
            user:JSON.parse( localStorage.getItem("user")),
            comments: []     
        }
    },
    props:{
        postID:{
            type:String,
            default:""
        },
        // id cua nguoi dang bai viet
        userID:{
            type:String,
        }
    },
    watch:{
        postID(newValue) {
            this.createdComments(newValue)
        }
    },
    methods:{
        /**
         * @description hàm thực hiện lấy comment theo id của bài viết có phân trang
         * @param {Guid} postID id của bài viết
         */
        async createdComments(postID){
            await new CommentService().getCommentByPostId(postID)
            .then(res => {
                this.comments = res
            })
            .catch(err => { 
                console.log(err)
            })
        },

        /**
         * @description hàm thực hiện thêm 1 comment vào comments để hiện thị cho người dùng ngay lập tức
         * @param {Object} comment comment sau khi được tạo thành công  
         */
        addCommentPostSucess(comment){
            this.comments.unshift(comment)
        },
        /**
         * @description hàm thực hiện xóa 1 đối tượng comment theo id trong mảng comments để hiện thị cho người dùng
         * ngay lập tức khi xóa thành công
         * @param {Guid} commentID id của comment vừa được xóa thành công
         */
        deleteSuccess(commentID) {
            this.comments = this.comments.filter(elem => {
                return elem.commentID != commentID
            })
        }
    }
}
</script>
<style scoped>
@import url('./comment.css');
</style>