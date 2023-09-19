<template>
    <div class="main-content">
        <router-view></router-view>
        <router-view name="post"></router-view>
        <router-view name='user-page'></router-view>
        <wb-paging v-if="isShowPaging" :numberOfRecords="isPostList ? postList.length : followList.length"
            :totalRecord="isPostList ? totalPost : totalFollow" :pageCurrent="isPostList ? pagePost : pageFollow"
            :pageSize="isPostList ? pageSizePost : pageSizeFollow" @updatePageSize="updatePageSize"
            @clickAfterPage="handlerclickPage($event)" @clickPrePage="handlerclickPage($event)"></wb-paging>
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
    computed: {
        postList() {
            return this.$store.state.postList;
        },
        pagePost() {
            return this.$store.state.pagePost;
        },
        pageSizePost() {
            return this.$store.state.pageSizePost;
        },
        totalPost() {
            return this.$store.state.totalPost
        },
        followList() {
            return this.$store.state.followList
        },
        pageFollow() {
            return this.$store.state.pageFollow;
        },
        pageSizeFollow() {
            return this.$store.state.pageSizeFollow;
        },
        totalFollow() {
            return this.$store.state.totalFollow
        },
        isPostList() {
            return this.$store.state.isPostList
        },
        isShowPaging() {
            return this.$store.state.isShowPaging
        },
        isSearch() {
            return this.$store.state.isSearch
        },
        textSearch() {
            return this.$store.state.textSearch
        },
        isFollowedList() {
            return this.$store.state.isFolowedList
        },
        isPostListOfUser(){
            return this.$store.state.isPostListOfUser
        },
        userIDSelected(){
            return this.$store.state.userIDSelected
        }
    },
    methods: {
        // hàm thực hiện trở về trang trước nó
        handlerclickPage(isClickPrePage) {
            if (this.isPostList) {
                // nếu đang ở danh sách bài viết thì sẽ cập nhật trang theo danh sách bài viết
                if (isClickPrePage) {
                    this.$store.commit('setPagePostCurrent', this.pagePost - 1)
                } else {
                    this.$store.commit('setPagePostCurrent', this.pagePost + 1)
                }
                if (this.isSearch) {
                    // nếu đang ở trạng thái tìm kiếm thì cập nhập theo danh sách bài viết phù hợp với ký tự tìm kiếm
                    this.$store.dispatch("getPostForSearch", { textSearch: this.textSearch, page: this.pagePost, pageSize: this.pageSizePost })
                }
                else if(this.isPostListOfUser) {
                    this.$store.dispatch("getPostOfUser",{
                    id:this.userIDSelected,
                    page:this.pagePost,
                    pageSize:this.pageSizePost
                });
                }else 
                    this.$store.dispatch("getPostListAsync", { page: this.pagePost, pageSize: this.pageSizePost })
            } else {
                if (isClickPrePage) {
                    this.$store.commit('setPageFollowCurrent', this.pageFollow - 1)
                } else {
                    this.$store.commit('setPageFollowCurrent', this.pageFollow + 1)
                }
                if(this.isFollowedList) {
                    this.$store.dispatch("getFollowedList", { page: this.pageFollow, pageSize: this.pageSizeFollow })
                }else {
                    this.$store.dispatch("getFollowingList", { page: this.pageFollow, pageSize: this.pageSizeFollow })
                }
            }
        },
        updatePageSize(newPageSize) {
            if(this.isPostList) {
                this.$store.commit('setPagePostSize', newPageSize)
                this.$store.commit('setPagePostCurrent', 1)
                if(this.isSearch) {
                    this.$store.dispatch("getPostForSearch", { textSearch: this.textSearch, page: this.pagePost, pageSize: this.pageSizePost })
                } else if(this.isPostListOfUser) {
                    this.$store.dispatch("getPostOfUser",{
                    id:this.userIDSelected,
                    page:this.pagePost,
                    pageSize:this.pageSizePost
                });
                }else
                this.$store.dispatch("getPostListAsync", { page: this.pagePost, pageSize: this.pageSizePost })

            }else {
                this.$store.commit('setPageFollowSize', newPageSize)
                this.$store.commit('setPageFollowCurrent', 1)
                if(this.isFollowedList) {
                    this.$store.dispatch("getFollowedList", { page: this.pageFollow, pageSize: this.pageSizeFollow })
                }else {
                    this.$store.dispatch("getFollowingList", { page: this.pageFollow, pageSize: this.pageSizeFollow })
                }
            }
        },
    },
    components: {
        UserForm, PostList, WBPost
    }
}
</script>
<style scoped>
@import url('./the-main.css');
</style>