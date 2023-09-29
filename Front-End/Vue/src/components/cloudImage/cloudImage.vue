<template>
    <div :style="styleCloudImage" >
        <slot></slot>
    </div>
</template>
<script>
import { storage } from "../../config/firebase-config.js"
  import { ref, getDownloadURL } from "firebase/storage"
export default {
    name:"cloud-image",

    data(){ 
        return{
            urlDefaultPost : 'https://nguyentiensy.000webhostapp.com/anonymous-user.jpg',
            borderRadius :"50%"
        }
    },

    mounted() {
        try{
            getDownloadURL(ref(storage,this.path)).then(downloadURL=> {
                this.urlDefaultPost = downloadURL 
            })
        }catch{
            console.log("cu tu tu")
        }
    },
    watch:{
        path(newValue){
            getDownloadURL(ref(storage,newValue)).then(downloadURL=> {
                this.urlDefaultPost = downloadURL 
            })
            this.styleCloudImage['background-image'] = `url(${this.urlDefaultPost})`
        }
    },
    props:{
        width:{
            default:"36px"
        },
        height:{
            default:"36px"
        },
        path:{
            type:String
        },
        isBorderRadius:{
            type: Boolean,
            default:true
        }
    },
    computed:{
        styleCloudImage(){
            return {
                width:this.width,
                height:this.height,
                'background-image': `url(${this.urlDefaultPost})`,
                'background-size': 'contain',
                'background-repeat' :'no-repeat',
                'border-radius' : this.borderRadius
            }
        }
    }
}
</script>
<style>
    
</style>