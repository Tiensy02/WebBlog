<template>
    <div class="main-content">
        <router-view></router-view>
        <router-view name="post"></router-view>
        <router-view name='user-page'></router-view>
        <wb-paging v-if="isShowPaging" :numberOfRecords="isPostList? postList.length :followList.length " 
        :totalRecord="isPostList? totalPost : totalFollow" :pageCurrent="isPostList? pagePost : pageFollow" 
        :pageSize="isPostList? pageSizePost : pageSizeFollow" @updatePageSize="updatePageSize" @clickAfterPage="clickAfterPage"
        @clickPrePage="clickPrePage"></wb-paging>
    </div>
</template>
<script>
import UserForm from '../../views/userPage/UserForm.vue';
import PostList from '../../views/posts/postList/PostList.vue';
import WBPost from '../../views/posts/post/WBPost.vue';
export default {
    name: "the-main",
    data() {
        return {
            postID: "",
            post: {},
            
        }

    },
    computed:{
        postList(){
            return this.$store.state.postList;
        },
        pagePost(){
            return this.$store.state.pagePost;
        },
        pageSizePost(){
            return this.$store.state.pageSizePost;
        },
        totalPost(){
            return this.$store.state.totalPost
        },
        followList(){
            return this.$store.state.followList
        },
        pageFollow(){
            return this.$store.state.pageFollow;
        },
        pageSizeFollow(){
            return this.$store.state.pageSizeFollow;
        },
        totalFollow(){
            return this.$store.state.totalFollow
        },
        isPostList(){
            return this.$store.state.isPostList
        },
        isShowPaging(){
            return this.$store.state.isShowPaging
        }
    },
    methods: {
        clickPrePage(){
            if(this.isPostList) {
                this.$store.commit('setPagePostCurrent', this.pagePost  - 1 )
                this.$store.dispatch("getPostListAsync", {page:this.pagePost, pageSize:this.pageSizePost})
            } else {
                this.$store.commit('setPageFollowCurrent', this.pageFollow  - 1 )
                this.$store.dispatch("getPostListAsync", {page:this.pagePost, pageSize:this.pageSizePost})
            }
        },
        updatePageSize(newPageSize){
            this.$store.commit('setPagePostSize',newPageSize)
            this.$store.commit('setPagePostCurrent', 1 )
            this.$store.dispatch("getPostListAsync", {page:this.pagePost, pageSize:this.pageSizePost})
        },
        clickAfterPage(){
            this.$store.commit('setPagePostCurrent', this.pagePost + 1 )
            this.$store.dispatch("getPostListAsync", {page:this.pagePost, pageSize:this.pageSizePost})
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