import BaseService from "./base-service";
class FollowService extends BaseService{
    constructor(){
        super("https://localhost:7070/api/v1/Follow")
    }
    /**
     * @description hàm thực hiện lấy danh sách người dùng đang theo dõi user có id userID
     * @param {GUID} userID id của người được theo dõi 
     * @returns 
     */
    async getUserFollowing(id, page, pageSize){
        const url = this.endpoint(`/${id}/user-following`)
        const params = {id, page, pageSize}
        const res = await this.baseAxios.get(url,{params})
        return res.data
    }
    /**
     * @description hàm thực hiện lấy danh sách người dùng mà người user có id userID đang follow
     * @param {GUID} userID id của người đi theo dõi
     * @returns 
     */
    async getUserFollowed(id, page, pageSize) {
        const url = this.endpoint(`/${id}/user-followed`)
        const params = {id, page, pageSize}
        const res = await this.baseAxios.get(url, {params})
        return res.data
    }
}
export default FollowService