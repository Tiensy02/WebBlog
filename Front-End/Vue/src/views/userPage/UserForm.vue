<template>
    <div class="modal">
        <div class="user-wrapper">
            <div class="user-form-header">
                {{ this.nameForm == this.$_WBEnum.MENU_USERS.LOGIN ? this.$t("userMenu.loginHeader")
                    : this.$t('userMenu.singnUpHeader') }}
                <wb-icon @handllerClickIcon="closeUserForm" iconName="icon-close"></wb-icon>
            </div>
            <div class="user-form">
                <text-input @updateValueSuccess="updateValueSuccess" @onValidateInput="onValidateInput"
                    v-model="userInfo.userName" :isLabel="true" :isRequest="true" :labelText="$t('userForm.userName')"
                    :textPlaceholder="$t('userForm.userName')"></text-input>
                <text-input @updateValueSuccess="updateValueSuccess" @onValidateInput="onValidateInput"
                    v-model="userInfo.userEmail" typeInput="email" v-if="nameForm == $_WBEnum.MENU_USERS.SIGN_UP"
                    :isLabel="true" :isRequest="true" :labelText="$t('userForm.email')"
                    :textPlaceholder="$t('userForm.email')"> </text-input>
                <text-input @updateValueSuccess="updateValueSuccess" @onValidateInput="onValidateInput"
                    v-model="userInfo.userPassword" typeInput="password" :isLabel="true" :isRequest="true"
                    :labelText="$t('userForm.password')" :textPlaceholder="$t('userForm.password')" 
                    :isValidate="nameForm == $_WBEnum.MENU_USERS.SIGN_UP"></text-input>
                <text-input @updateValueSuccess="updateValueSuccess" @onValidateInput="onValidateInput"
                    typeInput="confirm-password" v-if="nameForm == $_WBEnum.MENU_USERS.SIGN_UP" :isLabel="true"
                    :valuePassword="userInfo.userPassword" :isRequest="true" :labelText="$t('userForm.confirmPassword')"
                    :textPlaceholder="$t('userForm.confirmPassword')"></text-input>
                <div v-if="hasErrorLogin" class="text-error">{{ this.$t("validate.loginError") }}</div>
            </div>
            <wb-button @handlerClickButton="handlerSuccessForm" typeClassButton="btn-primary"
                :textButton='this.nameForm == this.$_WBEnum.MENU_USERS.LOGIN ? this.$t("userMenu.loginHeader") : this.$t("userMenu.singnUpHeader")'></wb-button>
            <div class="more-action" v-if="this.nameForm == this.$_WBEnum.MENU_USERS.LOGIN">
                <div class="text-action">{{ this.$t("userMenu.forgotPassword") }}</div>
                <div @click="openUserForm($_WBEnum.MENU_USERS.SIGN_UP)" class="text-action">{{
                    this.$t("userMenu.singnUpHeader") }}</div>
            </div>
        </div>
        <wb-loadding :isLoadding="isLoadding"></wb-loadding>
    </div>
</template>
<script>

import ToastMess from '../../components/ToastMess/ToastMess.vue';
import createToast from '../../helpper/createToastMess.js';
import UserService from '../../service/user-service.js'
import checkEmail from '../../service/check-email.js'
export default {
    name: "user-form",
    mounted() {
    },
    data() {
        return {
            userInfo: {
                userName: '',
                userEmail: '',
                userPassword: ''
            },
            userService: new UserService(),
            errorElement: [],
            isLoadding: false,
            hasErrorLogin:false,
        };
    },
    methods: {
        /**
         * 
         */
        handlerSuccessForm() {
            if(this.nameForm == this.$_WBEnum.MENU_USERS.LOGIN ) {
                this.isLoadding = true
                console.log(this.userInfo)
                this.userService.getUserByNameAndPassword(this.userInfo.userName, this.userInfo.userPassword)
                .then(res=> {
                    if(res == ""){
                        this.hasErrorLogin=true
                        this.isLoadding = false

                    }else{
                        if( localStorage.getItem("user") ) {
                            localStorage.removeItem("user")

                        } else{
                            const userInfo = res
                            localStorage.setItem("user",JSON.stringify(userInfo))
                        }
                        this.isLoadding = false
                        location.reload();
                    }
                })
                .catch(err => {
                    console.log(err)
                    this.isLoadding = false
                })
            }else {
                // thực hiện kiểm tra xem form có lỗi không
                if (this.errorElement.length == 0) {
                    this.isLoadding = true
                    // nếu form không lỗi, bắt đầu kiểm tra email có tồn tại hay không
                    checkEmail(this.userInfo.userEmail)
                        .then(res => {
                            this.isLoadding = false
                            // nếu email tồn tại thì gọi hàm kiểm tra xem email có tồn tại trên database hay không
                            if (res.status == "valid") {
                                this.userService.getUserByEmail(this.userInfo.userEmail)
                                    .then(res => {
                                        // nếu không tồn tại trên database thực hiện thêm mới
                                        if (res == "") {
                                            this.userService.post({
                                                userName: this.userInfo.userName,
                                                userEmail: this.userInfo.userEmail,
                                                userPassword: this.userInfo.userPassword
                                            })
                                                .then(res => {
                                                    createToast(this.$t("userMenu.signUpSuccess"), "success")
                                                    this.$emit("signUpSuccess")
                                                })
                                                .catch(err => {
                                                    console.log(err)
                                                })
                                        // nếu tồn tại trên database thì thực hiện đưa ra cảnh báo
                                        } else {
                                            createToast(this.$t('validate.emailExits'), "warm")
                                        }
                                    })
                                    .catch(err => {
                                        console.log(err)
                                    })
                            }else {
                                createToast(this.$t('validate.emailNotExits'), "danger")
                            }
                        })
                        .catch(err => {
                            this.isLoadding = false
                        })
                }
            }

        },
        /**
         * @description hàm thực hiện thêm phần tử lỗi vào mảng errorElement
         * @param {Object} errorObject đối tượng input gồm 2 thuộc tính: "name" input element, "errorMess" thông báo lỗi
         */
        onValidateInput(errorObject) {
            this.errorElement.push(errorObject.name);
        },
        /**
         * @description xoá các phần tử lỗi ra khỏi mảng errorElement khi hết lỗi
         * @param {Object} valueObject đối tượng input gồm 2 thuộc tính: "name" là element input, "newValue" giá trị của input
         */
        updateValueSuccess(valueObject) {
            this.errorElement = this.errorElement.filter(elem => {
                return elem != valueObject.name;
            });
        },
        closeUserForm() {
            this.$emit("closeUserForm");
        },
        /**
         * @description thực hiện mở form
         * @param {Number} nameForm chỉ định tên mở form đăng ký hay đăng nhập
         */
        openUserForm(nameForm) {
            this.$emit("openUserForm", nameForm);
        }
    },
    props: {
        nameForm: {
            default: 3
        }
    },
    components: { ToastMess }
}
</script>
<style scoped> @import url(./user-form.css);</style>