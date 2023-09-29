<template>
    <div v-for="commentRoot in commentRoots" :key="commentRoot.commentID"
        :class="!isCommentReply ? 'comment-respone-wrap' : 'comment-reply-wrap'">
        <div class="comment-respone-header ">
            <div class="post-user-avatar">
                <cloud-image :path="commentRoot.userAvatar"></cloud-image>
            </div>
            <div class="comment-user-info">
                <p class="user-name">{{ commentRoot.userName }}</p>
                <p class="comment-date">{{ this.converDateComment(commentRoot.createDate) }}</p>
            </div>
        </div>
        <div class="comment-respone-content">
            {{ commentRoot.commentContent }}
            <div class="comment-respone-action flex-row flex-col-gap8 ">
                <p @click="commentRoot.isReply = true" v-if="!commentRoot.isReply && user != null" class="reply-comment">{{
                    this.$t('userAction.reply') }}</p> 
                <p @click="deleteComment(commentRoot.commentID,commentRoot)" v-if="user != null && user.userID == commentRoot.userID" class="delete-comment">{{
                    this.$t("userAction.delete") }}</p>
            </div>
        </div>
        <CommentWrite :userCommentID="commentRoot.userID" :commentParentID="commentRoot.commentID" @addCommentResponeSucess="addCommentResponeSucess($event, commentRoot)" :propPostID="propPostID"
            @closeCommentRespone="commentRoot.isReply = false" v-if="commentRoot.isReply" typeComment="reply">
        </CommentWrite>
        <div v-if="commentRoot.numberCommentChild > 0">
            <p v-if="!commentRoot.isRenderComment" class="comment-child" @click="handleCommentChildClick(commentRoot)">
                {{ `${this.showNumberCommentChild(commentRoot.numberCommentChild)}` }}
            </p>
        </div>
        <comment-root  :propPostID="propPostID" :propCommentRoots="commentRoot.commentChilds"
            :isCommentReply="true"> 
        </comment-root>
    </div>
</template>
<script>
import createToast  from '../../helpper/createToastMess.js'    
import sprintf from 'sprintf-js'
import CommentService from '../../service/comment-service.js'
import CommentWrite from './CommentWrite.vue'
import convertDate from '../../helpper/convert-date.js'
export default {
    name: "comment-root",

    created() {
    },
    computed: {

    },

    data() {
        return {
            user: JSON.parse(localStorage.getItem("user")),
            commentRoots: this.propCommentRoots,
            postID: this.propPostID,
        }
    },
    props: {
        // danh sách comment cha
        propCommentRoots: {

        },
        // danh sách comment có phải là danh sách trả lời hay không, hay là danh sách comment cha
        isCommentReply: {
            type: Boolean,
            default: false
        },

        // id của bài viết
        propPostID: {

        }
    },
    watch: {
        propCommentRoots(newValue) {
            if (newValue instanceof Promise)
                newValue.then(res => {
                    this.commentRoots = res
                })
            this.commentRoots = newValue
        },

    },
    methods: {
        /**
         * @description hàm thực hiện chuyển định dạng ngày tháng
         * @param {DateTime} dateString dạng datetime ở database
         */
        converDateComment(dateString) {
            return convertDate(dateString)
        },

        onClickReply() {
            this.isReply = true
        },
        closeCommentRespone() {
            this.isReply = false
        },
        /**
         * @description hàm thực hiện show ra số comment con của 1 comment
         * @param {NUmber} number số comment con 
         */
        showNumberCommentChild(number) {
            return sprintf.sprintf(this.$t("userAction.showCommentChild"), number)
        },
       
        /**
         * @description hàm thực hiện lấy danh sách comment con của 1 comment cha
         * @param {Guid} commentParentID id của comment cha
         */
        async getCommentChilds(commentParentID) {
            var result = []
            await new CommentService().getCommentChild(commentParentID)
                .then(res => {
                    result = res
                })
                .catch(err => {
                    console.log(err)
                })
            return result
        },
        /**
         * @description hàm thực hiện lấy danh sách comment con cho comment cha
         * @param {Object} commentRoot đối tượng comment cha
         */
        handleCommentChildClick(commentRoot) {
            commentRoot.commentChilds = this.getCommentChilds(commentRoot.commentID);
            commentRoot.isRenderComment = true;
        },
 /**
         * @description hàm thực hiện thêm mới 1 comment trả lời cho commentRoots để hiện thị cho người dùng
         * @param {Object} newComment comment mới sau khi thêm thành công
         */
         addCommentResponeSucess(newComment,commentRoot) {
            commentRoot.isRenderComment = true;
            commentRoot.commentChilds = this.getCommentChilds(commentRoot.commentID)
            commentRoot.isReply = false
            
        },

        deleteComment(commentID,commentRoot){
            new CommentService().delete(commentID)
            .then(res => {
                if(this.isCommentReply == false) {
                    this.$emit("deleteSuccess",commentID)
                } else{
                    this.commentRoots = this.commentRoots.filter(elem => {
                        return  elem.commentID != commentID
                    })
                }
            })
            .catch(err => {
                console.log(err)
                createToast(this.$t("userAction.errorDeleteComment"),"danger")
            })
        }
    },
    components: {
        CommentWrite
    },
    emits: ['deleteSuccess']
}
</script>
<style scoped>@import url('./comment.css');</style>