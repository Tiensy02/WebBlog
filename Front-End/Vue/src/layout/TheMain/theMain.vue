<template>
    <div class="main-content">
        <router-view></router-view>
        <router-view name="post"></router-view>
        <router-view name='user-page'></router-view>
    </div>
</template>
<script>
import UserForm from '../../views/userPage/UserForm.vue';
import CommentService from '../../service/comment-service';
import PostList from '../../views/posts/postList/PostList.vue';
import WBPost from '../../views/posts/post/WBPost.vue';
import PostService from '../../service/post-service.js';
export default {
    name: "the-main",
    data() {
        return {
            isShowPostList: true,
            isShowPost: false,
            postID: "",
            post: {},
            
        }

    },
    methods: {
        async handlerClickPostAsync(postID) {
            console.log(postID)
            this.postID = postID
            await new PostService().getPostById(this.postID)
                .then(res => {
                    this.post = res
                    new CommentService().getCommentByPostId(this.postID)
                    .then(res => {
                        this.post.numberComment = res.totalComment
                        console.log(this.post.numberComment)
                    })
                    .catch(err=>{
                        console.log(err)
                    })
                })
                .catch(err => {
                    console.log(err)
                })
            this.isShowPostList = false
            this.isShowPost = true

        }
    },
    components: {
        UserForm, PostList, WBPost
    }
}
</script>
<style scoped>
@import url('./the-main.css');
</style>