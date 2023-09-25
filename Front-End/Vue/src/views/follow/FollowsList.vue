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
                <wb-button v-else moreClass="btn-following" textButton="Following"></wb-button>
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
        async getFollows(){
            this.isLoading = true
            this.$store.commit("setIsPostList",false)
             if(this.isFolowedList) {
                await this.$store.dispatch("getFollowedList", { id:this.userIDSelected,userCurrentID:this.userCurrent? this.userCurrent.userID : null,page: this.pageFollow, pageSize: this.pageSizeFollow })
            }else {
                await this.$store.dispatch("getFollowingList", {id:this.userIDSelected,userCurrentID:this.userCurrent? this.userCurrent.userID : null, page: this.pageFollow, pageSize: this.pageSizeFollow })
            }
            this.isLoading= false
        },
        clickFollowButton(isFollowed, follow){
            if(this.userCurrent){
                if(isFollowed== 0 ) {
                    var followAdd = {
                        userFollowID: this.userCurrent.userID,
                        userFollowed:follow.userID
                    }
                    new FollowService().post(followAdd)
                    .then(res => {
                        console.log("thanh cong");
                    })
                    .catch(err => {
                        console.log("that bai");
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