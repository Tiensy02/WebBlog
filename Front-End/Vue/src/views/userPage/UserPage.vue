<template>
    <div :key="$route.params.userID" class="user-page-wrap">
        <div class="user-info-wrap">
            <div class="user-info">
                <cloud-image width="52px" height = "52px" :path="user.userAvatar" ></cloud-image>
                <div class="user-name">   
                    {{ user.userName }}
                </div>
            </div>
            <div class="user-action">
                <wb-button v-if="userCurrent != null &&  userCurrent.userID == user.userID" typeClassButton="btn-secondary" :textButton="$t('userAction.fix')"></wb-button>
                <wb-button v-else-if="followObject == null " moreClass="btn-followed" typeClassButton="btn-primary" textButton="Follow"
                @handlerClickButton="clickFollowButton"></wb-button>
                <wb-button v-else moreClass="btn-following" textButton="Following" @handlerClickButton="clickFollowButton"></wb-button>
            </div>
        </div>
        <div class="user-page-list">
            <wb-button typeButton="buttonLink" :textButton="$t('userForm.post')" @handlerClickButton="clickItemPost" :moreClass="isPostListOfUser? 'btn-active' : ''"></wb-button>
            <wb-button typeButton="buttonLink" :textButton="$t('userForm.followed')" @handlerClickButton="clickItemFollowed" :moreClass="(isFolowedList && !isPostListOfUser)? 'btn-active' : ''"></wb-button>
            <wb-button typeButton="buttonLink" :textButton="$t('userForm.following')" @handlerClickButton="clickItemFollowing" :moreClass="(!isFolowedList && !isPostListOfUser)? 'btn-active' :''"></wb-button>
        </div>
        <div class="user-containt">
            <PostList v-if="isPostListOfUser"></PostList>
            <FollowsList v-else></FollowsList>
        </div>
        <wb-loadding :isLoadding="isLoading"></wb-loadding>
    </div>
</template>
<script>
import PostList from '../posts/postList/PostList.vue'
import createToast from '../../helpper/createToastMess.js'
import UserService from '../../service/user-service.js'
import FollowsList from '../follow/FollowsList.vue'
import FollowService from '../../service/follow-service.js'
import NotificationService from '../../service/notification-service.js'
import sprintf from 'sprintf-js'
export default {
    name:"user-page",
    async created(){
        this.$store.commit("setUserIDSelected",this.$route.params.userID)
        this.getUserbyUserID(this.userIDSelected)
        this.$store.commit('setIsShowPaging', true)
        this.$store.commit("setIsPostListOfUser",true)
        this.$store.commit("setPagePostCurrent",1)
        this.$store.commit("setPagePostSize",10)
        if (this.userCurrent != null) {
       await this.$store.dispatch("getUserCurrentFollowsAsync", {
        userCurrentID: this.userCurrent.userID
        })}
        this.checkUserFollow(this.user.userID)
    },
    beforeUnmount(){
        this.$store.commit("setIsPostListOfUser",false)
    },
    data(){
        return {
            userCurrent:JSON.parse(localStorage.getItem("user")),
            isShowPosts:true,
            isLoading:false,
            user:{},
            followObject:null, 
        }
    },

    props:{
        // id của người dùng được chọn
        propUserID:{
            type:String,
            default:""
        }
    },
    computed:{
        isPostList(){
            return this.$store.state.isPostList
        },
        isShowPaging(){
            return this.$store.state.isShowPaging
        },  
        isFolowedList(){
            return this.$store.state.isFolowedList
        },
        isPostListOfUser(){
            return this.$store.state.isPostListOfUser
        },
        isFolowedList(){
            return this.$store.state.isFolowedList 
        },
        follows(){
            return this.$store.state.followList
        },
        pageFollow(){
            return this.$store.state.pageFollow
        },
        pageSizeFollow(){
            return this.$store.state.pageSizeFollow
        },
        userIDSelected(){
            return this.$store.state.userIDSelected
        },
        userCurrentFollows(){
            return this.$store.state.userCurrentFollows
        }
    
    },
   
       
    methods:{
     

      /**
         * @description hàm thực hiện lấy thông tin của người dùng có id là userID
         * @param {GUID} userID id của người dùng 
         */
         async getUserbyUserID(userID){
            await new UserService().getByID(userID)
            .then(res => {
               this.user = res
            })
            .catch(err => {
                createToast(this.$t('validate.errorCommon'),"danger")
            })
        },
        // xử lý khi click vào button bài viết
        clickItemPost(){
            this.$store.commit("setIsPostListOfUser",true)
            
        },
        // xử lý khi click vào button người theo dõi
        async clickItemFollowing(){
            this.$store.commit("setIsPostListOfUser",false)
            this.isLoading = true
            this.$store.commit("setIsPostList",false)             
            await this.$store.dispatch("getFollowingList", {id:this.userIDSelected,userCurrentID:this.userCurrent? this.userCurrent.userID : null, page: this.pageFollow, pageSize: this.pageSizeFollow })         
            this.isLoading= false
        },
        // xử lý khi click vào button đang theo dõi
       async clickItemFollowed(){
            this.$store.commit("setIsPostListOfUser",false)
            this.isLoading = true
            this.$store.commit("setIsPostList",false)
            await this.$store.dispatch("getFollowedList", { id:this.userIDSelected,userCurrentID:this.userCurrent? this.userCurrent.userID : null,page: this.pageFollow, pageSize: this.pageSizeFollow })
            this.isLoading= false
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
        if(this.userCurrent != null ){

            if( this.followObject  == null ) {
                var followAdd = {
                            userFollowID: this.userCurrent.userID,
                            userFollowed:this.user.userID
                        }
                        new FollowService().post(followAdd)
                        .then(res => {
                            this.followObject = followAdd
                            new NotificationService().postNotification(this.user.userID,sprintf.sprintf(this.$t("notification.follow"),this.userCurrent.userName), 
                            this.$_WBEnum.NOTIFICATION.FOLLOW_NOTI)
                           
                        })
                        .catch(err => {
                            console.log(err);
                            createToast(this.$t('validate.errorCommon'), 'danger')
                        })
            } else {
                new FollowService().deleteFollowAsync(this.userCurrent.userID, this.user.userID)
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
    components:{
        PostList, FollowsList
    }
}
</script>
<style scoped>
    @import url('./user-page.css');
</style>