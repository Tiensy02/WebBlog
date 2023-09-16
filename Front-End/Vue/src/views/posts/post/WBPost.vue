<template>
    <div class="post-wrap">
        <div class="post-header">
            <div class="post-author flex-row" >
                <div class="post-user-avatar">
                    <cloud-image :path="post.userAvatar"></cloud-image>
                </div>
                <div class="author-info flex-row">
                    <div class="author-name">
                        {{ this.post.userName }}
                    </div>
                    <wb-button textButton="Follow" @handlerClickButton="folowUser(this.post.userID)" typeClassButton="btn-primary"></wb-button>
                </div>
            </div>
            <div class="post-info">
                <div class="post-date">
                    {{ this.converDatePost(this.post.createDate) }}
                </div>
                <div class="comment-number-wrap">
                    <wb-icon :isHover="false" iconName="icon-comment"></wb-icon>
                    <span class="comment-number">{{ this.post.numberCommentOfPost }} </span>
                </div>
            </div>
        </div>
       <div class="post-container">
            <div class="post-title">
                {{ this.post.postTitle }}
            </div>
            <div class="post-content">
                {{ this.post.postContent }}
            </div> 
       </div>
       <div class="post-bottom">

           <div class="rating-wrap">
            <p class="rating-title">
                {{this.$t('userAction.Rating')}} <span class="rating-number">{{ ` (${this.numberRating} ${this.$t('userAction.reviews')} )` }}</span>
            </p>
            <rating-star @ratingSuccess="ratingSuccess" :ratingCoreValue="post.postRatingCore" :postID="post.postID"> </rating-star>

           </div>
           <div class="post-comment">
                <Comments :postID="post.postID"></Comments>
           </div>
           <wb-loadding :isLoadding="isLoadding"></wb-loadding>
       </div>

    </div>
</template>
<script>
import Comments from '../../comments/Comments.vue';
import convertDate from '../../../helpper/convert-date.js';
import PostService from '../../../service/post-service.js';

export default {
    name:"wb-post",
    async created(){
        this.isLoadding = true
       await new PostService().getPostById(this.$route.params.postID)
        .then(res=> {
            this.post = res
            this.numberRating = res.postRattingNumber
        })
        .catch(err => {
            console.log(err)
        }) 
        this.isLoadding = false
    },
    data(){
        return{
          commentRoot:[],
          post:{},
          isLoadding : false,
          numberRating: 0
        }
    },
    methods:{
        folowUser(userID) {

        },
        converDatePost(dateString){
            return convertDate(dateString)
       },
       // hàm xử lý khi đánh giá thành công
       ratingSuccess(){
        this.numberRating ++
       }
    },
    watch:{
        
    },
    components:{
        Comments
    },
}
</script>
<style scoped>
    @import url(./post.css);
</style>