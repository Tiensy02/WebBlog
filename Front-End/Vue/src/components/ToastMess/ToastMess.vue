<template>
    <div v-if="isAppear" class="toast-mess-wrap">
        <WBIcon :iconName="`icon-${typeToastMess}`"></WBIcon>
        <div class="toast-mess-content">{{ contentMess }}</div>
    </div>
</template>

<script>
import WBIcon from '../Icons/WBIcon.vue';
export default {
    name: "toast-mess",
    data() {
        return{
            
        }
    },
    watch:{
      
    },
    components:{
        WBIcon
    },
    props:{
        contentMess:{
            type: String,
            default:"content Message",
        },
        isAppear:{
            type:Boolean,
            default:false
        },
        typeToastMess:{
            type:String,
            default:'success',
            validator : function(value) {
                return ['success',
                'warm',
                'danger'
            ]
            }
        }
    }
}
</script>
    
<style>
@import url('./toast-mess.css');
</style>