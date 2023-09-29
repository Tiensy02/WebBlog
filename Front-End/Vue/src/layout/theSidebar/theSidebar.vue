<template>
    <div class="sidebar-wrap"> 
        <div class="sidebar-elem">
            <p class="sidebar-elem-header"> {{ this.$t('common.newPost') }}</p>
            <div class="sidebar-item" v-for="(post , index) in posts" :key="index" >
                <p class="post-title" @click="handlerClickPost(post.postID)">
                    {{ post.postTitle }}
                </p>
                <hr>
            </div>
        </div>
        <div class="sidebar-elem">
            <p class="sidebar-elem-header">{{ this.$t('common.newComment') }}</p>
            <div class="sidebar-item" v-for="(comment , index) in comments" :key="index" >
                <div class="comment-wrap">
                    <cloud-image :path="comment.userAvatar"></cloud-image>
                    <div class="comment-info">
                        <p class="user-comment" @click="onClickUser(comment.userID)">
                            {{ comment.userName }}
                        </p>
                        <p class="comment-title">
                            {{ comment.commentContent }}
                        </p>
                    </div>
                </div>
                <hr>
            </div>
        </div>
    </div> 
</template>
<script>
import CommentService from '../../service/comment-service.js'
import PostService from '../../service/post-service.js'
export default {
    name:'the-sidebar',
    data(){
        return {
            comments:[],
            posts:[]
        }
    },
    async created(){
        await this.getPostList()
        this.getComments(5)    
    },
    methods:{
        /**
         * @description hàm thực hiện lấy 1 số bình luận mới nhất
         * @param {Number} numberComment số bình luận muốn lấy
         */
        async getComments(numberComment) {
            new CommentService().getNewComment(numberComment)
            .then(res => {
                this.comments = res
            })
            .catch(err => {
                this.comments = []
                console.log(err)
            })
        },
          /**
         * @description hàm thực hiện chuyển tới bài viết 
         * @param {Guid} postID id của bài viết
         */
         async handlerClickPost(postID) {
            await this.$router.push({name:'post',params:{postID:postID}});
            location.reload()
        },
        /**
         * @description hàm thực hiện lấy 5 bài viết mới nhất
         */
        async getPostList(){
            await new PostService().getPostListForUI(1,5)
            .then(res => {
             this.posts= res.posts
            })
            .catch(err => {
                console.log(err)
                this.posts = []
            })
        },
        onClickUser(userID){
            this.$router.push({name:'user-page', params:{userID:userID}})
        }
    }
}
</script>
<style scoped>
    @import url('./the-sidebar.css');
</style>