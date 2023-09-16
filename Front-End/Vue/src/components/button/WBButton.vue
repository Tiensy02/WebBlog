<template>
    <button class="btn" :disabled="isDisabled" :class="[getClassButton(typeButton),{'btn-disable': isDisabled}, typeClassButton]" :style="styleButton"
        @click="!isContextMenu? handlerClickButton() : toggleIsShowContextMenu()">
        <wb-icon v-if="iconName != ''"  :iconName="iconName"
            :disable="true"></wb-icon>
        <div v-if="textButton != ''" class="button-title">{{ textButton }}</div>
        <div v-if="typeButton == 'buttonCombo'" class="btn-more" @click="toggleIsShowContextMenu">
            <wb-icon iconName="icon-down" :disable="true"></wb-icon>
        </div>
        <context-menu v-if="(isContextMenu || (typeButton == 'buttonCombo')) && isShowContextMenu"
            :items="itemsForButton" :width="widthContextMenu" :isIcon="isIconContextMenu"
            @handllerClickItemOnContextMenu="handlerClickItemOfContextMenuOnButton($event)"
            @closeContextMenu="toggleIsShowContextMenu" ></context-menu>
    </button>
</template>
<script>
export default {
    name: "wb-button",
    data() {
        return {
            isShowContextMenu: false
        }
    },
    props: {
        /**
         * kiểu button:
         * 'buttonIcon': button chỉ có icon
         * 'buttonText': button chỉ có text
         * 'buttonWithIcon': button có cả icon và text
         * 'buttonCombo': có icon, text và nút dropdown để hiện contextMenu
         * 'buttonLink': button dùng để chuyển hướng trang
         */
        typeButton: {
            type: String,
            default: "buttonText",
            validator: function (value) {
                return [
                    "btn",
                    "buttonIcon",
                    "buttonText",
                    "buttonLink",
                    "buttonWithIcon",
                    "buttonCombo"
                ].includes(value)
            }
        },
        /**
         * button thực hiện thao tác gì
         * "btn-primary": button thực hiện thao tác chính
         * "btn-secondary": button thực hiện thao tác phụ
         * "btn-danger": button thực hiện thao tác nguy hiểm
         */
        typeClassButton: {
            type: String,
            default: "",
            validator: function (value) {
                return [
                    "",
                    "btn-primary",
                    "btn-secondary",
                    "btn-danger",
                ].includes(value)
            }
        },
        //tên icon dùng cho button
        iconName: {
            type: String,
            default: ""
        },
        // text của button
        textButton: {
            type: String,
            default: ""
        },
        // button có contextMenu hay không
        isContextMenu: {
            type: Boolean,
            default: false
        },
        height: {
            default: "28px"
        },
        // màu chữ của button nếu muốn tự chọn
        color:{
            
        },

         /**
         *  danh sách item chứa các đối tượng gồm các trường: id, content, info
         *  id: là id của item
         *  content: nội dung của item
         *  info: là thông tin về item là kiểu gì: normal, danger, warm
         */
        itemsForButton:{
            type:Array,
            default:()=>[]
        },
        // chiều dài của context menu nếu có
        widthContextMenu:{
            default:"200px"
        },
        // item trong context menu có icon hay không
        isIconContextMenu:{
            type:Boolean,
            default:false
        },
        isDisabled:{
            type:Boolean,
            default:false
        }
    },
    methods: { 
        getClassButton(value) {
            switch (value) {
                case "buttonIcon": return "btn-icon" 
                case "buttonText": return "btn-text"
                case "buttonLink": return "btn-link"
                case "buttonWithIcon": return "btn-with-icon"
                case "buttonCombo": return "btn-combo"
                default: return ""
            }
        },
        toggleIsShowContextMenu(e) {
            this.isShowContextMenu = !this.isShowContextMenu
            if(this.typeButton=="buttonCombo"){
                e.stopPropagation();
            }

        },
        handlerClickButton() {
            this.$emit("handlerClickButton")
        },
        handlerClickItemOfContextMenuOnButton(id){
            this.$emit("handlerClickItemOfContextMenuOnButton",id)
        }
    },
    computed: {
        styleButton() {
            return {
                height: this.height,
                color:this.color
            }
        }
    }
}
</script>
<style scoped>@import url('./button.css');</style>