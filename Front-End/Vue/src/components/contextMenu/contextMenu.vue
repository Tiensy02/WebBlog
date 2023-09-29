<template>
    <div v-if="isShowContextMenu" class="context-menu-wrapper" :style="styleContextMenu" ref="ContextMenu">
        <div class="context-menu-item" v-for="(item, index) in items" :key="index"
            :class="{ 'item-active': isActive(item[itemID]), 'item-danger': (item[itemInfo] == 'danger'), 'item-warm': (item[itemInfo] == 'warm') }"
            @click="(!isActive(item[itemID])) ? handllerClickItemOnContextMenu(item[itemID]) : null">
            <wb-icon v-if="isIcon" width="36px" height="36px" :disable="true" :iconName="item[itemIcon]"></wb-icon>
            {{ item[itemContent] }}
        </div>
    </div>
</template>
  
<script>
import { ref } from 'vue';

export default {
    name: "context-menu",
    mounted() {
        this.openContextMenu()
    },
    setup() {
        const ContextMenu = ref(null);
        return {
            ContextMenu,
        };


    },
    data() {
        return {
            isShowContextMenu: false
        }
    },
    props: {
        width: {
            type: String,
            default: "200px"
        },
        /**
         *  danh sách item chứa các đối tượng gồm các trường: id, content, info
         *  id: là id của item
         *  content: nội dung của item
         *  info: là thông tin về item là kiểu gì: normal, danger, warm
         */
        items: {
            type: Array,
            required: true
        },
        // tên của thuộc tính lưu trữ ID
        itemID: {
            type: String,
            default: "id"
        },
        // tên của thuộc tính lưu trữ content
        itemContent: {
            type: String,
            default: "content"
        },
        // tên của thuộc tính lưu trữ info
        itemInfo: {
            type: String,
            default: "info"
        },
        // các item có icon đi kèm hay không
        isIcon: {
            type: Boolean, 
            default: false
        },
        //tên của thuộc tính lưu trữ tên icon
        itemIcon: {
            type: String,
            default: "icon"
        },
        // id của item đang được active
        activeID: {
        }
    },
    methods: {
        /**
         * @description hàm thực hiện kiểm tra xem item có phải item đang được chọn hay không
         * @param {any} id id của item   
         */
        isActive(id) {
            if (id == this.activeID) return true
            else return false
        },
        /**
         * hàm thực hiện gọi đến hàm xử lí của component cha khi có sự có click vào item, và gửi cho component cha
         * id của item
         * @param {any} id id của item
         */
        handllerClickItemOnContextMenu(id) {
            this.$emit("handllerClickItemOnContextMenu", id)
        },
        clickOutContextMenu(e) {
            if (e.target != this.ContextMenu) {
                this.closeContextMenu()
            }
        },
        closeContextMenu() {
            this.$emit("closeContextMenu")
            window.removeEventListener("click", this.clickOutContextMenu)
        },
        openContextMenu() {
            this.isShowContextMenu = true
            setTimeout(() => {
                window.addEventListener("click", this.clickOutContextMenu);
            });
        }
    },
    computed: {
        styleContextMenu() {
            return {
                'min-width': this.width
            }
        }
    }
}
</script>
<style scoped>
@import url('./context-menu.css');
</style>