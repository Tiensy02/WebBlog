import BaseService from "./base-service";
class NotificationService extends BaseService{
    constructor(){
        super("https://localhost:7070/api/Notification")
    }
    async updateNotificationStatus(ids) {
        const url = this.endpoint('/Many')
        const res = await this.baseAxios.put(url,ids)
        return res
      }
    
      async getNotificationByUserID(userID) {
        const url = this.endpoint(`/${userID}/notification`);
        const res = await this.baseAxios.get(url);
        return res.data;
      }
}
export default NotificationService