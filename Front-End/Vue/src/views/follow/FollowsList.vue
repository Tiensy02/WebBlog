<template >
    <div class="follows-wrap">
        <div class="follow-items" v-for="follow in follows" :key="follow.followID">
            <div class="user-infor">
                <cloud-image width="32px" height = "32px" :path="follow.userAvatar" ></cloud-image>
                <p class="user-name">{{ follow.userName }}</p>
            </div>
            <wb-button v-if="follow.isFollowedByUser == 0 " typeClassButton="btn-primary" textButton="Follow"></wb-button>
            <wb-button v-else typeClassButton="btn-primary" textButton="Unfollow"></wb-button>
        </div>
    </div>
</template>
<script>
export default {
    name:"follow-list",
    data(){
        return {
            userCurrent :JSON.parse(localStorage.getItem("user"))
        }
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
        isFolowedList(newValue) {
            this.getFollows()
        }
    },
    methods:{
        getFollows(){
            if(this.isFolowedList) {
                this.$store.dispatch("getFollowedList", { id:this.userIDSelected,userCurrentID:this.userCurrent? this.userCurrent.userID : null,page: this.pageFollow, pageSize: this.pageSizeFollow })
            }else {
                this.$store.dispatch("getFollowingList", {id:this.userIDSelected,userCurrentID:this.userCurrent? this.userCurrent.userID : null, page: this.pageFollow, pageSize: this.pageSizeFollow })
            }
        }
    }

}
</script>
<style>
    
</style>