<template>
    <div class="input-wrap" :class="{ 'is-valid': hasError && isValidate }" @keyup.enter="isSearch ? searchInput() : null">
        <label v-if="isLabel" for="" :class="[{ request: isRequest }, 'form-label']">{{ labelText }}</label>
        <div :class="['input-box', { 'input-disabled': disabled }]" ref="inputElement">
            <wb-icon v-if="isIcon" :iconName=iconName></wb-icon>
            <input :type="getTypeInput(typeInput)" class="input" :placeholder="textPlaceholder" v-model="inputValue"
                ref="input" @focus="handlerFocus" :class="{ 'input-disabled': disabled }">
            <wb-icon v-if="isSearch" iconName="icon-search" @click="searchInput"></wb-icon>
            <wb-icon v-if="['password','confirm-password'].includes(typeInput) && inputValue!='' " 
            :iconName="isShowPassword? 'icon-eye':'icon-eye-close'" @handllerClickIcon="openPassword"></wb-icon>
        </div>
        <span v-if="hasError && isValidate" class="text-error-mess">{{ errorMess }}</span>
    </div>
</template>
<script>
import { ref } from 'vue';
import validateConfig from '../../config/config.js'
import { regexValidate, emptyValidate, validateConfirm } from '../../helpper/validate.js'
export default {
    name: 'text-input',
    mounted() {
        // thực hiện gửi đi phần tử input cho component cha xử lý
        this.$emit("sendInputElement", this.input)
    },
    setup() {
        const input = ref(null)
        return {
            input
        }
    },
    data() {
        return {
            inputValue: this.defaultValue,
            hasError: false,
            errorMess: "",
            isIconClose: false,
            isFocus: false,
            isShowPassword : false
        }
    },
    methods: {
        /**
         * @description thực hiện chọn tất cả giá trị của input khi đc focus vào lần đâu tiên
         */
        handlerFocus() {
            this.isFocus = true
            if (this.inputValue.trim() != "") {
                this.input.select()
            }
        },
        /**
         * @description thực hiện clear input khi click vào icon close
         */
        clearInput() {
            this.inputValue = ""
        },
        /**
        * @description :-nếu giá trị nhập vào rỗng thì gửi cho component cha 1 object chứa ô input lỗi, thông báo lỗi và hiện thị lỗi ra màn hình
        *               -nếu có giá trị thì cập nhập giá trị trong component cha và gửi cho component cha 1 object chứa ô input và giá trị input
        * @param {*} value giá trị ô input khi người dùng nhập vào
        */
        checkInput(value) {
            try {
                if (this.typeInput == "email") {
                    if(value.trim() != "") this.checkInputEmail(value)
                    else this.checkInputEmpty(value)
                } else if(this.typeInput == "password") {
                    if(value.trim() != "") {
                        this.checkInputPassword(value)
                    }
                    else {
                        this.checkInputEmpty(value)
                    }

                }else if(this.typeInput == "confirm-password") {
                    if(value.trim() != "") this.checkInputComfirmPassword(value)
                    else this.checkInputEmpty(value)
                }
                 else this.checkInputEmpty(value)
            }
            catch (e) {
                console.log("error in TextInput.vue ~ " + e)
            }
        },
        // thực hiện gọi hàm xử lí ở component cha và gửi giá trị ở ô input
        searchInput() {
            this.checkInput(this.inputValue)
            this.isFocus = false
            this.input.blur()
            this.$emit("searchInput", this.inputValue)
        },
        /**
         * @description hàm thực hiện kiểm tra lỗi không được để trống
         * @param {*} value giá trị của input
         */
        checkInputEmpty(value) {
            if (!emptyValidate(value, this.labelText).trim() == "") {
                this.sendErrorObject(emptyValidate(value, this.labelText))
            } else {
                this.sendValueInput(value)
            }
        },
        /**
         * @description hàm thực hiện gửi 1 đối tượng lỗi cho component cha xử lý
         * @param {string} errorMess nội dung thông báo lỗi
         */
        sendErrorObject(errorMess) {
            this.isIconClose = false
            this.hasError = true;
            let errorObject = {
                name: this.input, // thành phần input
                mess: this.errorMess
            }
            this.errorMess = errorMess
            this.$emit("onValidateInput", errorObject)
        },
        /**
         * @description hàm thực hiện gửi đi giá trị của input cho component cha khi không có lỗi
         * @param {string} value giá trị của input
         */
        sendValueInput(value) {
            this.isIconClose = true
            this.hasError = false;
            let valueObject = {
                name: this.input, // thành phần input
                newValue: value
            }
            this.$emit('update:modelValue',value)
            this.$emit("updateValueSuccess",valueObject)
        },
        /**
         * @description hàm thực hiện kiểu tra email
         * @param {String } value giá trị của input
         */
        checkInputEmail(value) {
            if (regexValidate(value, validateConfig.EmailRegex, this.$t("validate.emailError")) != '') {
                this.sendErrorObject(regexValidate(value, validateConfig.EmailRegex, this.$t("validate.emailError")))
            } else {
                this.sendValueInput(value)
            }
        },
        /**
         * @description hàm thực hiện kiểm tra mật khẩu
         * @param {string } value giá trị của input
         */
        checkInputPassword(value) {
            if (regexValidate(value, validateConfig.PassWordRegex, this.$t("validate.passwordError")) != '') {
                    this.sendErrorObject(regexValidate(value, validateConfig.PassWordRegex, this.$t("validate.passwordError")))
            
            } else {
                this.sendValueInput(value)
            }
        },
        /**
         * @description hàm kiểm tra mật khẩu nhập lại có chính xác không
         * @param {string} value giá trị của input
         */
        checkInputComfirmPassword(value) {

           if( validateConfirm(this.valuePassword,value,this.labelText) != "") {
            this.sendErrorObject(validateConfirm(this.valuePassword,value,this.labelText) )
           }else 
           this.sendValueInput(value)
        },
        /**
         * hàm lấy kiểu input cho thẻ input
         * @param {String} value tên kiểu input
         */
        getTypeInput(value) {
            switch (value) {
                case 'text': return 'text';
                case 'email': return 'text';
                case 'password': return 'password';
                case 'confirm-password': return 'password';
                case 'number': return 'number';
                case 'file': return 'file';
                default: return 'text';
            }
        },
        openPassword(){
            this.isShowPassword = ! this.isShowPassword
            if(this.isShowPassword) {
                this.input.setAttribute('type','text')
            }else this.input.setAttribute('type','password')
        }
    },
    props: {
        /**placeholder của input */
        textPlaceholder: {
            type: String,
            default: "nhập dữ liệu"
        },
        // input có icon hay không
        isIcon: {
            type: Boolean,
            default: false
        },
        // nếu có thì nhập tên icon
        iconName: {
            type: String,
            default: ""
        },
        // input có phải ô tìm kiếm không
        isSearch: {
            type: Boolean,
            default: false
        },

        /**trên ô tìm kiếm có label không */
        isLabel: {
            type: Boolean,
            default: false
        },
        /** nội dung của label */
        labelText: {
            type: String,
            default: ""
        },

        /** trường dữ liệu có bắt buộc phải nhập không */
        isRequest: {
            type: Boolean,
            defaule: false
        },
        /** dữ liệu dữ liệu ban đầu*/
        defaultValue: {
            type: String,
            default: ""
        },
        /** trường tìm kiếm bình thường thì sẽ không có cần validate */
        isValidate: {
            type: Boolean,
            default: true
        },
        // input có phải disable không
        disabled: {
            type: Boolean,
            default: false
        },
        // kiểu của input
        typeInput: {
            type: String,
            default: "text",
            validator: function (value) {
                return [
                    'text',
                    'password',
                    'confirm-password',
                    'number',
                    'email',
                    'file'
                ].includes(value)
            }
        },
        // giá trị của mật khẩu 
        valuePassword:{
            type:String,                
        }
    },
    watch: {
        /**kiểm tra mỗi khi giá trị input thay đổi  */
        inputValue(newValue) {
            if(this.isValidate){
                this.checkInput(newValue);
            } else {
                this.sendValueInput(newValue)
            }
        },
    },
}
</script>

<style scoped>
@import url('./input.css');
</style>