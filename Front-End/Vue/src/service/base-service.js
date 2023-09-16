import Axios from "axios"
import "regenerator-runtime/runtime";
class BaseService {
    constructor(baseUrl) {
        this.baseUrl=baseUrl
        this.baseAxios = Axios
    }
    endpoint(url) {
        return this.baseUrl + url
    }
     /**
   * lấy toàn bộ dữ liệu
   * @returns danh sách bản ghi
   */
  async getAll() {
    const url = this.baseUrl;
    const res = await this.baseAxios.get(url)
    return res
}
/**
 * @description hàm thực hiện lấy 1 bản ghi theo id
 * @param {*} id 
 * @returns 
 */
    async getByID(id){
        const params = {id}
        const res = await this.baseAxios.get(this.baseUrl + `/${id}`,params)
        return res.data
    }
/**
 * * thực hiện thêm mới dữ liệu
 * @param {Object} dataAdd đối tượng được thêm
 */
async post(dataAdd) {
    const url = this.baseUrl
    const res = await this.baseAxios.post(url, dataAdd);
    return res
}

/**
 * * thực hiện cập nhật dữ liệu của bản ghi
 * @param {Object} dataUpdate đối tượng cần được sửa
 */
async put(dataUpdate) {
    const res = await this.baseAxios.put(this.baseUrl, dataUpdate);
    return res;
}

/**
 * xóa 1 bản ghi
 * @param {string} id id của bản ghi dạng guid
 */
async delete(id) {
    const res = await this.baseAxios.delete(this.baseUrl + `/${id}`);
    return res
}

/**
 * xóa nhiều bản ghi
 * @param {Array} ids mảng chứa các id của bản ghi
 */
async deleteMultiple(ids) {
    const url = this.endpoint('/Many')
    const res = await this.baseAxios.delete(url, {
      headers: {
        'Content-Type': 'application/json' // Đặt header Content-Type là application/json để chỉ định dữ liệu gửi đi là JSON
      },
      data: ids 
    });
    return res
}

}

export default BaseService;
