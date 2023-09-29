<template >
    <div class="follows-wrap">
        <div v-if="follows.length > 0" class="follow-items" v-for="follow in follows" :key="follow.followID">
            <div class="user-avatar" @click="onClickUser">
                <cloud-image width="32px" height = "32px" :path="follow.userAvatar" ></cloud-image>
            </div>
            <div class="user-info">
                <p class="user-name">{{ follow.userName }}</p>
                <wb-button v-if="follow.isFollowedByUser == 0 " moreClass="btn-followed" typeClassButton="btn-primary" textButton="Follow"
                @handlerClickButton="clickFollowButton(follow.isFollowedByUser,follow)"></wb-button>
                <wb-button v-else moreClass="btn-following" textButton="Following" @handlerClickButton="clickFollowButton(follow.isFollowedByUser,follow)"></wb-button>
            </div>
        </div>
        <div v-else class="list-empty"> 
            {{ this.$t("common.emptyList") }}
        </div>
    </div>
</template>
<script>
import FollowService from '../../service/follow-service.js'
import createToast from '../../helpper/createToastMess.js';
import NotificationService from '../../service/notification-service.js'
import sprintf from 'sprintf-js'
export default {
    name:"follow-list",
    data(){
        return {
            userCurrent :JSON.parse(localStorage.getItem("user")),
            isLoading:false
        }
    },
    mounted(){
        console.log(this.isFolowedList);
    },
    computed:{
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
        }
    },
    watch:{
       
    },
    methods:{
       /**
        * @description hàm thực hiện folow hoặc unfollow khi click vào các button tương ứng
        * @param {Numbe } isFollowed trạng thái có user hiện tại có đang follow hay không
        * @param {Object} follow đối tượng follow
        */
        clickFollowButton(isFollowed, follow){
            if(this.userCurrent){
                if(isFollowed== 0 ) {
                    var followAdd = {
                        userFollowID: this.userCurrent.userID,
                        userFollowed:follow.userID
                    }
                    new FollowService().post(followAdd)
                    .then(res => {
                        follow.isFollowedByUser = 1
                        new NotificationService().postNotification(follow.userID,sprintf.sprintf(this.$t("notification.follow"),this.userCurrent.userName), 
                        this.$_WBEnum.NOTIFICATION.FOLLOW_NOTI)
                    })
                    .catch(err => {
                        console.log(err);
                        createToast(this.$t('validate.errorCommon'), 'danger')
                    })
                    
                } else {
                    
                        new FollowService().deleteFollowAsync(this.userCurrent.userID,follow.userID)
                        .then(res => {
                            follow.isFollowedByUser = 0
                        })
                        .catch(err => {
                            createToast(this.$t('validate.errorCommon'), 'danger')
                        })
                    
                   
                }
            }else {
                createToast(this.$t('errorHandle.loginError'), 'danger')
            }
        }
    }

}
</script>
<style scoped>
    @import url('./follows.css');
</style>