<template>
    <div class="header-wrap flex-row">
        <div class="header-left flex-row flex-center" @click="backToHome">
            <div class="logo-image"></div>
            <div class="logo-title">SHARE</div>
        </div>
        <div class="header-center">
            <text-input @searchInput="searchInput" :isSearch="true" :isValidate="false" :textPlaceholder="$t('common.search')"></text-input>
        </div>
        <div class="header-right flex-row flex-center flex-col-gap12">
            <div class="notiffication-wrapper" @click="showNotification"> 
                <div v-if="getNotificationNotRead() > 0" class="icon-count">{{ this.getNotificationNotRead() }}</div>
                <wb-icon iconName="icon-notifi" :isHover="true"></wb-icon> 
                <WBNotification v-if="isShowNotification" :propNotifications="this.notifications"></WBNotification>
            </div>
            <wb-icon iconName="icon-write" :isHover="true" @handllerClickIcon="isShowPostForm = !isShowPostForm"></wb-icon>
            <wb-button typeButton="buttonWithIcon" :isContextMenu="true" :iconName="iconNameLang" color="white"
                :textButton="textButtonLang" :itemsForButton="langCodes" widthContextMenu="50px" :isIconContextMenu="true"
                @handlerClickItemOfContextMenuOnButton="toggleLanguage($event)"></wb-button>
                <cloud-image v-if="user" :path="user.userAvatar" >
                    <wb-icon :iconName="(user)?  user.userAvatar : 'icon-avatar' " width="36px" height="36px" :isContextMenu="true" :isIconContextMenu="true"
                :itemOfIcon="hasUser? menuUserLogOut : menuUserLogin" widthContextMenu="50px"
                @handlerClickItemOfContextMenuOnIcon="handlerUserInfo($event)"></wb-icon>
                </cloud-image>
                <wb-icon v-else :iconName="(user)?  user.userAvatar : 'icon-avatar' " width="36px" height="36px" :isContextMenu="true" :isIconContextMenu="true"
                :itemOfIcon="hasUser? menuUserLogOut : menuUserLogin" widthContextMenu="50px"
                @handlerClickItemOfContextMenuOnIcon="handlerUserInfo($event)"></wb-icon>
            
        </div>
    </div>
    <postForm v-if="isShowPostForm && user" @closePostForm="closePostForm"></postForm>
    <UserForm @signUpSuccess="signUpSuccess" :name-form="nameForm" v-if="isShowUserform" @closeUserForm="closeUserForm" @openUserForm="openUserForm($event)"></UserForm>
</template>
<script>
import WBNotification from '../../components/Notification/WBNotification.vue';
import UserForm from '../../views/userPage/UserForm.vue';
import postForm from '../../views/postForm/postForm.vue';
import PostService from '../../service/post-service.js'
import NotificationService from '../../service/notification-service.js';
export default {
    name: "the-header",
    async mounted() { 
        if (this.user) {

            await new NotificationService().getNotificationByUserID(this.user.userID)
               .then(res=> {
                   this.notifications = res
               })
               .catch(err => {
                   console.log(err)
               })
        }
    },
    components: {
        UserForm, WBNotification,postForm
    },
    data() {
        return {
            iconNameLang: `icon-${localStorage.getItem("langCode")}`,
            textButtonLang: localStorage.getItem("langCode"),

            langCodes: [{ id: this.$_WBEnum.LANG_CODE.VIET_NAM, content: "Tiếng Việt", icon: "icon-vi" },
            { id: this.$_WBEnum.LANG_CODE.ENGLISH, content: "English", icon: "icon-en" }],

            menuUserLogin: [
            { id: this.$_WBEnum.MENU_USERS.LOGIN, content: this.$t("userMenu.loginHeader"), icon: "icon-login" },
            ],
            menuUserLogOut: [
                { id: this.$_WBEnum.MENU_USERS.USER_PAGE, content: this.$t("userMenu.userPage"), icon: "icon-user-page" },
                { id: this.$_WBEnum.MENU_USERS.LOG_OUT, content: this.$t("userMenu.logOut"), icon: "icon-log-out" },
            ],

            isShowUserform: false,
            hasUser: localStorage.getItem("user"),
            nameForm: this.$_WBEnum.MENU_USERS.LOGIN,
            user: JSON.parse( localStorage.getItem('user')),
            notifications:[],
            isShowNotification:false,
            isShowPostForm:false
        }
    },
    methods: {
        /**
         * @description hàm thực hiện tìm kiếm
         * @param {String} value giá trị ô input
         */
        searchInput(value){
            if( value.trim() != "") {
                new PostService().getPostByFilter(value)
                .then(res => {
                    this.$store.commit("setPostList",res)
                })
                .catch(err =>{
                    console.log(err)
                })
            }else {
                new PostService().getPostListForUI(1,this.$store.state.pageSizePost)
                .then (res => {
                    this.$store.commit("setPostList",res.posts)
                })
                .catch(err => {
                    console.log(err) 
                })
            }
        },
        /**
         * @description hàm thực hiện thay đổi ngôn ngữ
         * @param {any} id id nhận được từ contextMenu
         */
        toggleLanguage(id) {
            if (id == this.$_WBEnum.LANG_CODE.VIET_NAM) {
                if (localStorage.getItem("langCode") != "vi") {
                    this.$store.commit("setLangCode", "vi")
                }
            } else if (id == this.$_WBEnum.LANG_CODE.ENGLISH) {
                if (localStorage.getItem("langCode") != "en") {
                    this.$store.commit("setLangCode", "en")
                }
            }
        },
        /**
         * @description hàm thực hiện lấy ra số lượng thông báo chưa được đọc
         */
        getNotificationNotRead(){
            var countNotifi = 0
            this.notifications.forEach(elem => {
                if( elem.notificationStatus == 0) {
                    countNotifi++
                }
            }
            )
            return countNotifi
        },
        /**
         * @description hàm thực hiện gọi mở danh sách thông báo của người dùng
         */
        async showNotification(){
            if (this.user) {
                await new NotificationService().getNotificationByUserID(this.user.userID)
                .then(res=> {
                    this.notifications = res
                    this.isShowNotification = !this.isShowNotification
                })
                .catch(err => {
                    console.log(err)
                })
            }else {
                this.isShowNotification = !this.isShowNotification
            }
        },
        closePostForm(){
            this.isShowPostForm=false
        },
        /**
         * @description hàm thực hiện xử lý sự kiện click vào menu User
         * @param {Number} id id của contextMenu    
         */
        async handlerUserInfo(id) {
            if (id == this.$_WBEnum.MENU_USERS.LOGIN) {
                this.nameForm = this.$_WBEnum.MENU_USERS.LOGIN
                this.isShowUserform = true
            } else if(id == this.$_WBEnum.MENU_USERS.LOG_OUT) {
                localStorage.removeItem("user")
                await this.$router.push({name:"home"})
                location.reload()
            } else {
                this.$router.push({name:"user-page",params:{userID:this.user.userID}})
            }
        },
        closeUserForm() {
            this.isShowUserform = false
        },
        openUserForm(nameForm) {
            this.nameForm =nameForm
            this.isShowUserform = true
        },
        /**
         * @description hàm thực hiện các thao tác khi đăng ký thành công
         */
        signUpSuccess(){
            this.closeUserForm()
            this.openUserForm(this.$_WBEnum.MENU_USERS.LOGIN)
        },
        backToHome(){
            this.$router.push({name:'home'});
        }
    }
}
</script>
<style>
@import url('./the-header.css');
</style>