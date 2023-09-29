<template>
    <div class="text-area-wrap">
        <label v-if="isLabel" for="" class="form-label">{{ labelText }}</label>
        <textarea ref="textArea" name="" :class="[{'has-error' :isValidate && errMess !='' }, 'form-textarea']" maxlength="60000"
            :placeholder="(disabled) ? '' : $t('userAction.writeResponse')" :rows="rowNumber" v-model="valueTextAre"
            :disabled="disabled" @blur="validateValue(valueTextAre)">
        </textarea>
        <div v-if="isValidate && errMess !=''  " class="validate-mess">{{ this.errMess }}</div>
    </div>
</template>

<script>
import {ref} from 'vue' 
import {  emptyValidate } from '../../helpper/validate.js'
export default {
    name: "m-text-area",
    setup(){
        const textArea = ref(null)
        return{
            textArea
        }
    },
    mounted() {
        // thực hiện gửi dữ liệu ban đầu cho component cha
        let dataInput = {
            name: this.textArea,
            newValue: this.valueText
        }
        this.$emit("updateTextAreaValue", dataInput)
        // thực hiện gửi textarea element cho component cha xử lí
        this.$emit("sendTextAreaElement", this.textArea)
    },
    data() {
        return {
            valueTextAre: this.valueText,
            errMess:''
        }
    },
    watch: {
        /**
         * @description thực hiện cập nhập dữ liệu khi có thay đổi
         * @param {String} newValue giá trị của text area
         */
        valueTextAre(newValue) {
         this.validateValue(newValue)
        },
        valueText(newValue){
            if(newValue.trim() == "") {
                this.valueTextAre = ""
            }
        }
    },
    methods:{
        validateValue(value){
            let dataInput = {
                name: this.textArea,
                newValue: value
            }
            this.$emit("updateTextAreaValue", dataInput)
            this.$emit('update:modelValue',value)
            if(emptyValidate(value,this.labelText) != "" ) {
                this.errMess = emptyValidate(value,this.labelText)
            }else {
                this.errMess = ""
            }

        }
    },
    props: {

        isLabel: {
            type: Boolean,
            default: false
        },
        labelText: {
            type: String
        },
        disabled: {
            type: Boolean,
            default: false
        },
        valueText: {
            type: String,
            default: ""
        },
        rowNumber:{
            type:Number,
            default:4
        },
        isValidate:{
            type:Boolean,
            default:false
        }
    }
}
</script>

<style scoped>
@import url('./textarea.css');
</style>