<template>
    <div class="modal" v-if="isShowDialog">
        <div class="dialog-wrap">
            <div class="dialog-main">
                <div class="dialog-header flex">
                    <div class="dialog-title">{{ dialogTitle }}</div>
                    <wb-icon iconName="icon-close" @handllerClickIcon="closeDialog"></wb-icon>
                </div>
                <div class="dialog-content" v-html="dialogMess"></div>
            </div>
            <div class="dialog-bottom" ref="dialogBottom">
                <wb-button :textButton="textButtonPrimary" v-if="hasMoreButton" typeClassButton="btn-danger" @handlerClickButton="handlerClickButtonPrimary"></wb-button>
                <wb-button :textButton="$t('common.button.close')" @handlerClickButton="closeDialog"></wb-button>
            </div>
        </div>
    </div>
</template>
<script>
import { ref } from 'vue';
export default {
    setup(){
        const dialogBottom = ref(null);
        return {
            dialogBottom
        }
    },
    mounted(){
        this.dialogBottom.children[0].focus()
    },
    data() {
        return {
            isShowDialog:true
        }
    },

    props: {
        // tiêu đề của dialog
        dialogTitle: {
            type: String,
            default: ""
        },
        // nôi dung thông báo
        dialogMess: {
            type: String,
            default: ""
        },
        //tên button thực hiện thao tác quan trọng
        textButtonPrimary:{
            type:String,
            default:""
        },
        // dialog có cần thêm button thực hiện hành động hay không=
        hasMoreButton:{
            type:Boolean,
            default:false
        }

    },
    methods: {
        closeDialog() {
            this.isShowDialog = false
        },
        handlerClickButtonPrimary(){
            this.$emit("handlerClickButtonPrimary")
        }
        
    }

}
</script>
<style scoped>
    @import url('./dialog.css');
</style>