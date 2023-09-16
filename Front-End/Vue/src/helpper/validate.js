import WBResource from '../resourse/resourse.js';
import store from '../store/store.js'
import sprintf from 'sprintf-js'

/**
 * @description hàm thực hiện validate giá trị trống
 * @param {String} valueCheck giá trị của đôi tượng cần kiểm trả
 * @param {String} nameField tên trường được kiểm tra
 * @returns {String} thông báo lỗi
 */
function emptyValidate(valueCheck,nameField){
    if(valueCheck.trim() == "" ){
        return sprintf.sprintf(WBResource[store.state.langCode].validate.emptyError,nameField)
    }else return ""
} 

/**
 * hàm kiểm tra theo regex
 * @param {String} valueCheck giá trị của đối tượng cần kiểm tra
 * @param {String} errmess thông báo lỗi
 * @param {String} regexName tên regex đc sử dụng tên đặt ở config.js
 * @returns thông báo lỗi
 */
function regexValidate(valueCheck,regexName,errmess){
    if(!regexName.test(valueCheck) && valueCheck != "") {
        return errmess
    } else return ""
}

/**
 * hàm kiểm tra xem trường được kiểm tra có giá trị trùng với trị của đối tượng đc yêu cầu hay không ( vd kiểm tra nhập lại mật khẩu)
 * @param {String} valueCheck giá trị của đối tượng cần kiểm tra
 * @param {String} valueConfirm giá trị của đối tượng được xét có giống với giá trị của valueCheck không
 * @param {String} nameField tên trường được kiểm tra 
 * @returns thông báo lỗi 
 */
function validateConfirm(valueCheck,valueConfirm,nameField){
    if(valueCheck != valueConfirm ) {
        return sprintf.sprintf(WBResource[store.state.langCode].validate.confirmError,nameField)
    }else return ""
}

export {regexValidate,emptyValidate,validateConfirm};




