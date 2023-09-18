<template>
    <div class="user-page-wrap">
        <div class="user-info-wrap">
            <div class="user-info">
                <cloud-image width="52px" height = "52px" :path="userCurrent.userAvatar" ></cloud-image>
                <div class="user-name">
                    {{ userCurrent.userName }}
                </div>
            </div>
            <div class="user-action">
                <wb-button v-if="userCurrent != null &&  userCurrent.userID == user.userID" typeClassButton="btn-secondary" :textButton="$t('userAction.fix')"></wb-button>
                <wb-button v-else-if="!userCurrent || userCurrent.userID != user.userID" typeClassButton="btn-secondary" :textButton="$t('userMenu.notiFollow')"></wb-button>
                <wb-button v-else :textButton="$t('userAction.unFollow')" typeClassButton="btn-secondary" :isDisabled="true"></wb-button>
            </div>
        </div>
        <div class="user-page-list">
            <wb-button typeButton="buttonLink" :textButton="$t('userForm.post')"></wb-button>
            <wb-button typeButton="buttonLink" :textButton="$t('userForm.followed')"></wb-button>
            <wb-button typeButton="buttonLink" :textButton="$t('userForm.following')"></wb-button>
        </div>
        <div class="user-containt">
            <div class="user-content">
            </div>
            <div class="user-info-table">

            </div>
        </div>
    </div>
</template>
<script>

import createToast from '../../helpper/createToastMess.js'
import FollowService from '../../service/follow-service.js'
import UserService from '../../service/user-service.js'
export default {
    name:"user-page",
    created(){
        this.getUserbyUserID(this.propUserID)
        this.getUsersFolloing(this.propUserID, 1, 10)
        this.getUsersFollowed(this.propUserID, 1, 10)
        this.$store.commit('setIsShowPaging', true)
    },

    data(){
        return {
            userCurrent:JSON.parse(localStorage.getItem("user")),
            user:{},
            usersFollowed:[], // danh sách người dùng được this.user follow
            usersFollowing:[] // danh sách người dùng follow this.user
        }
    },

    props:{
        // id của người dùng được chọn
        propUserID:{
            type:String,
            default:"6601b158-505b-11ee-a114-0673c2320d10"
        }
    },
    computed:{
        isPostList(){
            return this.$store.state.isPostList
        },
        isShowPaging(){
            return this.$store.state.isShowPaging
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
                res = this.user
            })
            .catch(err => {
                createToast(this.$t('validate.errorCommon'),"danger")
            })
        },
        /**
         * @description hàm gán danh sách người dùng được user theo dõi
         * @param {GUID} userID id của người dùng
         */
        async getUsersFollowed(userID,page,pageSize) {
            await new FollowService().getUserFollowed(userID,page,pageSize)
            .then(res => {
                this.usersFollowed = res
            })
            .catch(err => {
                console.log(err)
                createToast(this.$t('validate.errorCommon'),"danger")
            })
        },
        /**
         * @description hàm lấy danh sách người dùng đang theo dõi user
         * @param {GUID} userID id của người dùng
         */
        async getUsersFolloing(userID,page,pageSize) {
            await new FollowService().getUserFollowing(userID,page,pageSize)
            .then(res => {
                this.usersFollowing = res
            })
            .catch(err => {
                console.log(err)
                createToast(this.$t('validate.errorCommon'),"danger")
            })
        }
    }
}
</script>
<style scoped>
    @import url('./user-page.css');
</style>