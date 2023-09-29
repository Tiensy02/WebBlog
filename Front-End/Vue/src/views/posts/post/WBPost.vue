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
                <wb-button v-if="followObject == null " moreClass="btn-followed" typeClassButton="btn-primary" textButton="Follow"
                @handlerClickButton="user? clickFollowButton() : null"></wb-button>
                <wb-button v-else moreClass="btn-following" textButton="Following" @handlerClickButton="user? clickFollowButton() : null"></wb-button>
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
            <rating-star :numberRatingOfPost="post.postRattingNumber" @ratingSuccess="ratingSuccess" :ratingCoreValue="post.postRatingCore" :postID="post.postID"> </rating-star>
           </div>
           <div class="post-comment">
                <Comments :userID="post.userID" :postID="post.postID"></Comments>
           </div>
           <wb-loadding :isLoadding="isLoadding"></wb-loadding>
       </div>
    </div>
</template> 
<script>
import Comments from '../../comments/Comments.vue';
import convertDate from '../../../helpper/convert-date.js';
import PostService from '../../../service/post-service.js';
import FollowService from '../../../service/follow-service.js';
import NotificationService from '../../../service/notification-service.js';
import createToast from '../../../helpper/createToastMess.js';
import sprintf from 'sprintf-js'

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
        this.$store.commit('setIsShowPaging',false)
        this.$store.commit('setIsPostList',false)
        if (this.user != null) {
      await this.$store.dispatch("getUserCurrentFollowsAsync", {
        userCurrentID: this.user.userID
        })}
        this.checkUserFollow(this.post.userID)
        
    },
    data(){
        return{
          commentRoot:[],
          post:{},
          isLoadding : false,
          numberRating: 0,
          user: JSON.parse(localStorage.getItem("user")),
          followObject:null, // đối tượng follow 
        }
    },
    computed:{
        userCurrentFollows(){
            return this.$store.state.userCurrentFollows
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
        new NotificationService().postNotification(this.post.userID, sprintf.sprintf(this.$t('notification.ratting'),this.user.userName),
        this.$_WBEnum.NOTIFICATION.RATING_NOTI)
       },
       /**
        * @description hàm thực hiện kiểm tra user đang đăng nhập có theo dõi chủ bài viết hay chưa nếu có trả về đối tượng follow, nếu không return null
        * @param {GUID} userID id của người đăng bài viết
        */
       checkUserFollow(userID) {
        if(this.userCurrentFollows != null ) {
            this.userCurrentFollows.forEach((elem) => {
                if(elem.userID == userID) {
                   this.followObject = elem
                }
            });
        }
       },
       clickFollowButton(){
        if(this.user != null ){

            if( this.followObject  == null ) {
                var followAdd = {
                            userFollowID: this.user.userID,
                            userFollowed:this.post.userID
                        }
                        new FollowService().post(followAdd)
                        .then(res => {
                            this.followObject = {
                            userFollowID: this.user.userID,
                            userFollowed:this.post.userID,
                            userID:this.post.userID,
                        }
                            new NotificationService().postNotification(this.post.userID,sprintf.sprintf(this.$t("notification.follow"),this.user.userName), 
                            this.$_WBEnum.NOTIFICATION.FOLLOW_NOTI)
                           
                        })
                        .catch(err => {
                            console.log(err);
                            createToast(this.$t('validate.errorCommon'), 'danger')
                        })
            } else {
                new FollowService().deleteFollowAsync(this.user.userID, this.post.userID)
                            .then(res => {
                                this.followObject = null
                                
                            })
                            .catch(err => {
                                createToast(this.$t('validate.errorCommon'), 'danger')
                            })
            }
        }
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