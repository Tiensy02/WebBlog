<template>
    <div>
        <div v-if="user != null" class="comment-respone-wrap flex-column">
            <div class="comment-respone">
                <div class="post-user-avatar">
                    <cloud-image :path="user.userAvatar"></cloud-image>
                </div>
                <text-area @updateTextAreaValue="updateTextAreaValue" :valueText="comment.commentContent"></text-area>
            </div>
            <div class="comment-action">
                <wb-button @handlerClickButton="closeCommentRespone" v-if="typeComment != 'post'"
                    typeClassButton="btn-danger" height="36px" :textButton="$t('userAction.discard')"></wb-button>
                <wb-button @handlerClickButton="onClickPost" typeClassButton="btn-primary" :isDisabled="!isPost"
                    height="36px"
                    :textButton="typeComment == 'post' ? $t('userAction.postComment') : $t('userAction.reply')"></wb-button>
            </div>
        </div>
        <div v-else class="comment-mess-wrap">
            <p class="comment-mess">{{ this.$t("userAction.loginToComment") }}</p>
        </div>
    </div>
</template>
<script>
import { v4 as uuidv4 } from 'uuid';
import CommentService from '../../service/comment-service.js'
import createToast from '../../helpper/createToastMess.js'
export default {
    name: "comment-write",
    data() {
        return {
            user: JSON.parse(localStorage.getItem("user")),
            comment: {
                commentContent: ""
            },
            isPost: false,
        }
    },
    props: {
        typeComment: {
            type: String,
            default: "post"
        },
        propPostID: {
            required: true
        },
        commentParentID: {
            type: String,
            default: ""
        }
    },
    methods: {
        updateTextAreaValue(objectValue) {
            this.comment.commentContent = objectValue.newValue
            if (objectValue.newValue.trim() != "") {
                this.isPost = true
            } else this.isPost = false
        },
        onClickPost() {
            var newComment = {
                commentID: uuidv4(),
                userID: this.user.userID,
                postID: this.propPostID,
                commentContent: this.comment.commentContent
            }

            if (this.typeComment == "post") {
                console.log(newComment)
                new CommentService().postCommetRoot(newComment)
                    .then(res => {
                        newComment['userAvatar'] = this.user.userAvatar
                        newComment['userName'] = this.user.userName
                        newComment['createDate'] = this.createDate()
                        this.$emit("addCommentPostSucess", newComment)
                        this.comment.commentContent = ""
                    })
                    .catch(err => {
                        createToast(this.$t("userAction.errorComment"), "danger")
                    })
            } else {
                newComment['commentParentID'] = this.commentParentID
                new CommentService().post(newComment)
                    .then(res => {
                        newComment['userAvatar'] = this.user.userAvatar
                        newComment['userName'] = this.user.userName
                        newComment['createDate'] = this.createDate()
                        this.$emit("addCommentResponeSucess", newComment)
                        this.comment.commentContent = ""
                    })
                    .catch(err => {
                        console.log(err)
                    })
            }
        },
        closeCommentRespone() {
            this.$emit("closeCommentRespone")
        },
        createDate() {
            var currentDate = new Date();

            // Lấy thông tin về năm, tháng, ngày, giờ, phút, giây
            var year = currentDate.getFullYear();
            var month = ('0' + (currentDate.getMonth() + 1)).slice(-2);
            var date = ('0' + currentDate.getDate()).slice(-2);
            var hours = ('0' + currentDate.getHours()).slice(-2);
            var minutes = ('0' + currentDate.getMinutes()).slice(-2);
            var seconds = ('0' + currentDate.getSeconds()).slice(-2);

            // Định dạng lại chuỗi ngày theo định dạng mong muốn
            var formattedDate = year + '-' + month + '-' + date + 'T' + hours + ':' + minutes + ':' + seconds;
            return formattedDate
        }
    }
}
</script>
<style scoped>
@import url('./comment.css');
</style>