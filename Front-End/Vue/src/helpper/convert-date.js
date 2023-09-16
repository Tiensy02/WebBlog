/**
 * @description hàm thực hiện chuyển gí trị date ở datababse về dạng ngày tháng năm
 * @param {String} dateString giá trị date ở dạng string
 * @returns định dạng ngày/ tháng / năm
 */
const convertDate = function(dateString){
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();
    return `${day}/${month}/${year}`
}
export default convertDate