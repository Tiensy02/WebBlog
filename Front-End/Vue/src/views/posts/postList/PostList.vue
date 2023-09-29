<template>
    <div class="post-list-wrapper" ref="postList">
        <div class="post-list" v-for="(post) in Posts" :key="post.postID" ref="post">
            <div class="post-info">
                <div class="post-user-avatar" @click="handlerClickUser(post.userID)">
                    <cloud-image :path="post.userAvatar"></cloud-image>
                </div>
                <div class="post-info-header">
                    <div class="post-author-wrap">
                        <div class="post-author">
                            {{ post.userName }}

                        </div>
                        <div class="post-date">
                            {{ this.converDatePost(post.createDate) }}
                        </div>
                        <wb-icon v-if="isPostListOfUser && userCurrent != null && userCurrent.userID == post.userID"
                            iconName="icon-more-action"
                            @handlerClickItemOfContextMenuOnIcon="handlerClickItemOfContextMenuOnIcon($event, post)"
                            :isContextMenu="true" :itemOfIcon="postActions" widthContextMenu="100px"></wb-icon>
                    </div>
                    <div class="post-title" @click="handlerClickPost(post.postID)">
                        {{ post.postTitle }}
                    </div>
                </div>
            </div>
            <div class="post-more-info">
                <div class="ratting-core">
                    <rating-star :PropIsRating="false" :ratingCoreValue="post.postRatingCore"
                        :postID="post.postID"></rating-star>
                    {{ ` ( ${post.postRattingNumber} )` }}
                </div>
                <div class="comment-number">
                    <wb-icon iconName="icon-comment"></wb-icon>
                    {{ post.numberCommentOfPost }}
                </div>

            </div>
        </div>
        <wb-loadding :isLoadding="isLoadding"></wb-loadding>
        <wb-dialog v-if="isShowDialog" :dialogTitle="$t('userAction.deletePost')" :dialogMess="$t('warn.deletePost')"
            :hasMoreButton="true" :textButtonPrimary="$t('userAction.delete')"
            @handlerClickButtonPrimary="deletePost"></wb-dialog>
    </div>
    <postForm :form-name="$_WBEnum.POST_ACTION.FIX" v-if="isShowPostForm" :postTitleDefaule="postTitle"
        :postContentDefault="postContent" :postID="postID" @closePostForm="closePostForm"></postForm>
</template>
<script>
import postForm from '../../postForm/postForm.vue';
import converDate from '../../../helpper/convert-date.js';
import PostService from '../../../service/post-service.js';
import createToast from '../../../helpper/createToastMess.js';
import { ref } from 'vue';
export default {
    name: "post-list",
    setup() {
        const postList = ref(null);
        const post = ref(null);
        return {
            postList, post
        };
    },

    async mounted() {
        this.getPostList()
        // thực hiện set trạng thái phân trang
        this.$store.commit('setIsShowPaging', true)
        this.$store.commit('setIsPostList', true)
        if (this.isPostListOfUser) {
            this.postList.style.height = "100%"
            this.postList.style.marginLeft = "0"
        } else {
            this.postList.style.height = "calc(100%  - 40px)"
            this.postList.style.marginLeft = "170px"
        }
        
    },
    data() {
        return {
            isLoadding: false,
            postActions: [
                { id: this.$_WBEnum.POST_ACTION.FIX, content: this.$t('userAction.fix') },
                { id: this.$_WBEnum.POST_ACTION.DELETE, content: this.$t('userAction.delete'), info: "danger" },
            ],
            userCurrent: JSON.parse(localStorage.getItem('user')),
            isShowPostForm: false,
            postTitle: "",
            postContent: '',
            postID: "",
            isShowDialog: false
        };

    },
    computed: {
        Posts() {
            return this.$store.state.postList;
        },
        pagePost() {
            return this.$store.state.pagePost;
        },
        pageSizePost() {
            return this.$store.state.pageSizePost;
        },
        isPostList() {
            return this.$store.state.isPostList
        },
        isShowPaging() {
            return this.$store.state.isShowPaging
        },
        isPostListOfUser() {
            return this.$store.state.isPostListOfUser
        },
        userIDSelected(){
            return this.$store.state.userIDSelected
        }
    },
    methods: {
        /**
         * @description hàm thực hiện chuyển định dạng ngày phía backend thành ngày/tháng/năm
         * @param {String} dateString định dạng ngày tháng nhận được từ backend
         */
        converDatePost(dateString) {
            return converDate(dateString);
        },
        /**
         * @description hàm thực hiện chuyển tới bài viết 
         * @param {Guid} postID id của bài viết
         */
        async handlerClickPost(postID) {
            this.$router.push({ name: 'post', params: { postID: postID } });
        },
        /**
         * @description hàm thực hiện chuyển đến trang cá nhân của người dùng được chọn
         * @param {GUID} userID id của người dùng được chọn
         */
        handlerClickUser(userID) {
            this.$router.push({ name: 'user-page', params: { userID: userID } })
        },
        /**
         * @description hàm thực hiện chuyển điểm về dạng từ 1 - 5, phần thập phân nếu có sẽ là phần .5
         * @param {double} core điểm đánh giá của bài viết nhận được từ database
         */
        converRatingCore(core) {
            core = Number(core)
            var newCore = Number(core.toFixed(2))
            if (newCore % 1 == 0) {
                return newCore
            } else {

                var coreDecimal = (newCore % 1).toFixed(2);
                if (coreDecimal <= 0.25) {
                    return newCore - coreDecimal
                } else if (coreDecimal <= 0.5) {
                    return newCore + (0.5 - coreDecimal)
                } else {
                    return newCore.toFixed(0)
                }
            }
        },
        /**
         * @description hàm thực hiện lấy danh sách bài viết để sử dụng cho component postList
         */
        async getPostList() {
            this.isLoadding = true;
            if (this.isPostListOfUser) {
                await this.$store.dispatch("getPostOfUser", {
                    id: this.$route.params.userID,
                    page: this.pagePost,
                    pageSize: this.pageSizePost
                });
            } else {
                await this.$store.dispatch("getPostListAsync", {
                    page: this.pagePost,
                    pageSize: this.pageSizePost
                });
            }
            this.isLoadding = false;
        },
        handlerClickItemOfContextMenuOnIcon(id, post) {
            if (id == this.$_WBEnum.POST_ACTION.DELETE) {
                this.isShowDialog = true
                this.postID = post.postID
            }
            else if (id = this.$_WBEnum.POST_ACTION.FIX) {
                this.postID = post.postID
                this.postTitle = post.postTitle
                this.postContent = post.postContent
                this.isShowPostForm = true
            }
        },
        deletePost() {
            new PostService().delete(this.postID)
                .then(res => {
                    createToast(this.$t('userAction.success'), "success")
                    this.$store.commit("setIsPostListOfUser", true)
                    this.$store.dispatch("getPostOfUser", {
                        id: this.userIDSelected,
                        page: this.pagePost,
                        pageSize: this.pageSizePost
                    });
                    this.isShowDialog = false
                })
                .catch(err => {
                    console.log(err);
                    createToast(this.$t('validate.errorCommon'), "danger")
                })
        },
        closePostForm() {
            this.isShowPostForm = false

        }
    },
    components: {
        postForm
    }
}
</script>
<style scoped>@import url('./post-list.css');</style>