<template>
    <div v-if="isLoadding" class="modal">
        <div class="loader"></div>
    </div>
</template>
<script>
export default {
    name:'wb-loadding',
    props:{
        isLoadding:{
            type:Boolean,
            default:false
        }
    }
}
</script>
<style scoped>
@import url(./loadding.css);
</style>