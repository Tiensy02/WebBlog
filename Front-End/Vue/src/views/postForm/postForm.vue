<template>
    <div class="modal">
        <div class="post-form-wrap">
            <div class="post-form-title">
                <text-area v-model="post.postTitle" @sendTextAreaElement="sendTextAreaElement" :isValidate="true"
                    @updateTextAreaValue="updateTextAreaValue" :isLabel="true" :labelText="$t('userForm.postTitle')"
                    :rowNumber="2"></text-area>

            </div>
            <div class="post-form-content">
                <text-area v-model="post.postContent" @sendTextAreaElement="sendTextAreaElement" :isValidate="true"
                    @updateTextAreaValue="updateTextAreaValue" :isLabel="true" :labelText="$t('userForm.postContent')"
                    :rowNumber="16"></text-area>
            </div>
            <div class="post-form-action">
                <wb-button typeClassButton="btn-danger" height="36px" :textButton="$t('userAction.discard')"
                    @handlerClickButton="handlerClickButton"></wb-button>
                <wb-button typeClassButton="btn-primary" height="36px" :textButton="$t('userAction.post')"
                    @handlerClickButton="handlerClickButton"></wb-button>
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
            postElemnt: []
        }
    },
    computed:{
        pagePost(){
            return this.$store.state.pagePost;
        },
        pageSizePost(){
            return this.$store.state.pageSizePost;
        },
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
        handlerClickButton() {
            if (this.errorElemments.length == 0) {
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
            }
            else {
                this.errorElemments.forEach(elem => {
                    console.log(elem)
                })
            }
        },
        sendTextAreaElement(elem) {
            this.postElemnt.push(elem)
        }

    }
}
</script>
<style scoped>@import url(./post-form.css);</style>