<template>
    <div class="icon-wrapper"
    :style="styleIcon"
    :title="tooltip"
    :class="{'icon-no-hover': !isHover}"
    @click="isContextMenu? toggleIsShowContextMenu(): handllerClickIcon()"
    >
        <div class="WB-icon" :class="iconName"></div>
        <context-menu v-if="isContextMenu && isShowContextMenu"
            :items="itemOfIcon" :width="widthContextMenu" :isIcon="isIconContextMenu"
            @handllerClickItemOnContextMenu="handlerClickItemOfContextMenuOnIcon($event)" 
            @closeContextMenu="toggleIsShowContextMenu"></context-menu>
    </div>
</template>
<script>
export default {
    name:"wb-icon",
    props:{
        // chiều dài của icon
        width:{
            type: String,
            default: "24px"
        },
        // chiều rộng của icon
        height:{
            type:String,
            default:"24px"
        },
        // tên icon
        iconName:{
            type:String,
            required:true
        },
        // tooltip cho icon
        tooltip:{
            type:String,
            default:""
        },
        // icon có phải disable không
        disable:{
            type:Boolean,
            default:false
        },
        // icon khi hover có hiện background không
        isHover:{
            type:Boolean,
            default:false
        },
        isContextMenu:{
            type:Boolean,
            default:false
        },
        widthContextMenu:{

        },
        isIconContextMenu:{
            type:Boolean,
            default:false
        },
        itemOfIcon:{
            type:Array,
            default:()=>[]
        }

    },
    data(){
        return {
            isShowContextMenu: false
        }
    },
    computed:{
        styleIcon(){
            return{
                width:this.width,
                height:this.height,
                display:"flex",
                alignItems:"center",
                justifyContent: "center", 
                flex: `0 0 ${this.width}`,
                cursor: this.disable ? "default" :"pointer"
            }
        }
    },
    methods:{
        handllerClickIcon(){
            this.$emit("handllerClickIcon")
        },
        handlerClickItemOfContextMenuOnIcon(id){
            this.$emit("handlerClickItemOfContextMenuOnIcon",id)
        },
        toggleIsShowContextMenu() {
            this.isShowContextMenu = !this.isShowContextMenu
        },
    }
}
</script>
<style scoped>
@import url(./icon.css);
</style>