<template>
    <div class="modal">
        <div class="post-form-wrap">
            <div class="post-form-title">
                <text-area v-model="post.postTitle" @sendTextAreaElement="sendTextAreaElement" :isValidate="true"
                    @updateTextAreaValue="updateTextAreaValue" :isLabel="true" :labelText="$t('userForm.postTitle')"
                    :rowNumber="2" :valueText="postTitleDefaule"></text-area>

            </div>
            <div class="post-form-content">
                <text-area v-model="post.postContent" @sendTextAreaElement="sendTextAreaElement" :isValidate="true"
                    @updateTextAreaValue="updateTextAreaValue" :isLabel="true" :labelText="$t('userForm.postContent')"
                    :rowNumber="16" :valueText="postContentDefault"></text-area>
            </div>
            <div class="post-form-action">
                <wb-button typeClassButton="btn-danger" height="36px" :textButton="$t('common.button.close')"
                    @handlerClickButton="closeForm"></wb-button>

                <wb-button v-if="formName==$_WBEnum.POST_fORM.NEW_POST" typeClassButton="btn-primary" height="36px" :textButton="$t('userAction.post')"
                    @handlerClickButton="onClickAction"></wb-button>
                <wb-button v-else typeClassButton="btn-primary" height="36px" :textButton="$t('userAction.fix')" @handlerClickButton="onClickAction"> </wb-button>

            </div>
        </div>
    </div>
</template>
<script>
import PostService from '../../service/post-service.js'
import createToast from '../../helpper/createToastMess.js';
export default {
    name: "post-form",
    mounted() {
        this.post.postTitle = this.postTitleDefaule
        this.post.postContent = this.postContentDefault
        this.sendTextAreaElement()
        this.postElemnt[0].focus()
        
    },
    data() {
        return {
            post: {
                postTitle: '',
                postContent: ''
            },
            errorElemments: [],
            postElemnt: [],
            
        }
    },
    props:{
        postTitleDefaule:{
            type:String,
            default:""
        },
        postContentDefault:{
            type:String,
            default:""
        },
        formName:{
            type:Number,
            default:1 
        },
        postID:{
            type:String,
            default:""
        }
    },
    
    computed:{
        pagePost(){
            return this.$store.state.pagePost;
        },
        pageSizePost(){
            return this.$store.state.pageSizePost;
        },
        userIDSelected(){
            return this.$store.state.userIDSelected
        }
    },
    methods: {
        updateTextAreaValue(datainput) {

            if (datainput.newValue.trim() == "") {
                this.errorElemments.push(datainput.name)

            } else {
                this.errorElemments = this.errorElemments.filter(ele => {
                    return ele != datainput.name
                })
            }
        },
        // hàm thực hiện hành động của form, thêm mới hoặc sửa bài viết
        onClickAction() {
            // nếu không có lỗi thì thực hiện hành động
            if (this.errorElemments.length == 0) {
                // nếu là form thêm mới thì thực hiện thêm mới bài viết
                if(this.formName==this.$_WBEnum.POST_fORM.NEW_POST) {

                    this.post.userID = JSON.parse(localStorage.getItem("user")).userID
                    new PostService().post(this.post)
                        .then(res => {
                            createToast(this.$t('userMenu.signUpSuccess'), "success")
                            this.$store.dispatch("getPostListAsync", {
                                page: this.pagePost,
                                pageSize: this.pageSizePost
                            });
                            this.$emit("closePostForm")
                        })
                        .catch(err => {
                            console.log(err)
                        })
                // nếu là form sửa thì thực hiện sửa bài viết
                }else {
                    this.post.postID = this.postID
                    new PostService().put(this.post)
                    .then(res => {
                        createToast(this.$t('userMenu.signUpSuccess'), "success")
                        this.$store.commit("setIsPostListOfUser", true)
                            this.$store.dispatch("getPostOfUser", {
                                id: this.userIDSelected,
                                page: this.pagePost,
                                pageSize: this.pageSizePost
                            });
                            this.$emit("closePostForm")
                    })
                    .catch(err => {
                        console.log(err)
                    })
                }
            }
            else {
                this.errorElemments.forEach(elem => {
                })
            }
        },
        closeForm(){
            this.$emit("closePostForm")
        },
        sendTextAreaElement(elem) {
            this.postElemnt.push(elem)
        }
    }
}
</script>
<style scoped>@import url(./post-form.css);</style>