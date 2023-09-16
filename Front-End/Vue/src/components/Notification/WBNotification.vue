<template>
    <div class="notification-wrap">
        <div class="notification-header">
            {{ this.$t("userForm.notification") }}
        </div>
        <div v-if="notifications.length == 0" class="notification-nothing">
            {{ this.$t('userMenu.notiNothing') }}
        </div>
        <div v-else class="notification-list">
            <div class="notification-item" v-for="notifi in notifications" :key="notifi.notificationID">
                <div class="notification-item-header">
                    {{ this.getNotificationTitle(notifi.notificationType) }}
                </div>
                <div class="notification-content">
                    {{ notifi.notificationContent }}
                </div>
                <div class="notification-date">
                    {{ this.getDateNotifi(notifi.createDate)  }}
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import convertDate from '../../helpper/convert-date.js'
import NotificationService from '../../service/notification-service.js'
export default {
    name:"wb-notification",
    data(){
        return {
            notifications :this.propNotifications
        }
    },
    mounted(){
        var listID = []
        this.notifications.forEach(elem => {
            listID.push(elem.notificationID)
        })
        new NotificationService().updateNotificationStatus(listID)
        .then(res=>{
            console.log(res)
        })
        .catch(err => {
            console.log(err)
        })
    },
    props:{
        propNotifications:{
            type:Array,
            default:()=>[]
        },
        
    },
    methods:{
        getNotificationTitle(notificationType){
            if(notificationType == this.$_WBEnum.NOTIFICATION.FOLLOW_NOTI) {
                return this.$t("userMenu.notiFollow")
            }else if (notificationType == this.$_WBEnum.NOTIFICATION.RATING_NOTI) {
                return this.$t("userMenu.notiRatting")
            } else {
                return this.$t("userMenu.notifiComment")
            }
        },
        getDateNotifi(dateString) {
           return convertDate(dateString)
        }
    }
}
</script>
<style scoped>
@import url('./notification.css');
</style>