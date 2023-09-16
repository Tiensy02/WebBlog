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
                            {{ this.converDatePost(post.createDate)  }}
                        </div>
                    </div>
                    <div class="post-title" @click="handlerClickPost(post.postID)">
                        {{ post.postTitle }}
                    </div>
                </div>
            </div>
            <div class="post-more-info">
                <div class="ratting-core">
                    <rating-star :PropIsRating="false" :ratingCoreValue="post.postRatingCore" :postID="post.postID"></rating-star>
                    {{ ` ( ${post.postRattingNumber} )` }}
                </div>
                <div class="comment-number">
                    <wb-icon iconName="icon-comment"></wb-icon>
                    {{ post.numberCommentOfPost }}
                </div>
               
            </div>
        </div>
        <wb-loadding :isLoadding="isLoadding"></wb-loadding>
        <wb-paging></wb-paging>
    </div>
</template>
<script>
import converDate from '../../../helpper/convert-date.js';
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
        this.isLoadding = true;
        await this.$store.dispatch("getPostListAsync", {
            page: 1,
            pageSize: 10
        });
        this.isLoadding = false;
    },
    data() {
        return {
            isLoadding: false,
            
        };

    },
    computed: {
        Posts() {
            return this.$store.state.PostList;
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
            this.$router.push({name:'post',params:{postID:postID}});
        },
        handlerClickUser(userID) {

        },
        /**
         * @description hàm thực hiện chuyển điểm về dạng từ 1 - 5, phần thập phân nếu có sẽ là phần .5
         * @param {double} core điểm đánh giá của bài viết nhận được từ database
         */
        converRatingCore(core) {
            core = Number(core)
            var newCore = Number(core.toFixed(2))
            if(newCore % 1  == 0 ) {
                return newCore 
            } else{

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
    },
}
</script>
<style scoped>@import url('./post-list.css');</style>