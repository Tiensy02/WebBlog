import BaseService from "./base-service";
class UserService extends BaseService{
    constructor(){
        super("https://localhost:7070/api/User")
    }
   /**
   * Lấy thông tin người dùng bằng email
   * @param {string} email Địa chỉ email người dùng
   * @returns Thông tin người dùng
   */
  async getUserByEmail(email) {
    const encodedEmail = encodeURIComponent(email);
    const url = this.endpoint(`/${encodedEmail}/Users`);
    const res = await this.baseAxios.get(url);
    return res.data
  }
  /**
   * @description hàm thực hiện lấy user theo tên và mật khẩu
   * @param {String} userName tên user
   * @param {String } userPassword mật khâu 
   * @returns
   */
  async getUserByNameAndPassword(userName, userPassword) {
    const url = this.endpoint('/UserName')
    const params = {userName,userPassword}
    const res = await this.baseAxios.get(url, {params} );
    return res.data
  }
}
export default UserService

