import BaseService from "./base-service";
class NotificationService extends BaseService {
  constructor() {
    super("https://localhost:7070/api/Notification")
  }
  async updateNotificationStatus(ids) {
    const url = this.endpoint('/Many')
    const res = await this.baseAxios.put(url, ids)
    return res
  }

  async getNotificationByUserID(userID) {
    const url = this.endpoint(`/${userID}/notification`);
    const res = await this.baseAxios.get(url);
    return res.data;
  }
  /**
   * 
   * @param {GUID} userID id của người nhận thông báo
   * @param {String} notificationContent nội dung thông báo
   * @param {Number} notificationType kiểu thông báo 
   */
  async postNotification(userID,notificationContent,notificationType){
    let notificationAdd = {
      userID:userID,
      notificationContent:notificationContent,
      notificationType: notificationType
  }
  new NotificationService().post(notificationAdd)
  .then(res => {
      console.log("gui thong bao thanh cong");
  })
  .catch(err => {
      console.log("loi khi gui thong bao")
  })
  }
}
export default NotificationService