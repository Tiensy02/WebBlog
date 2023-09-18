<template>
    <div class="footer-wrap">
         <div class="footer-element">
            <div class="footer-elem-header">
                {{ $t('common.resource') }}
            </div>
            <div class="footer-elem-content">
                <div class="footer-elem-item">
                    {{ $t('userForm.post') }}
                </div>
                <div class="footer-elem-item">
                    {{ $t('common.feedBack') }}
                </div>
                <div class="footer-elem-item">
                    {{ $t('common.help') }}
                </div>
            </div>
         </div>
         <hr>
         <div class="footer-element">
            <div class="footer-elem-header">
                {{ $t('common.links') }}
            </div>
            <div class="footer-elem-content">
                <div class="footer-elem-item">
                     Facebook
                </div>
                <div class="footer-elem-item">
                    GitHub
                </div>
                <div class="footer-elem-item">
                    Google
                </div>
            </div>
         </div>
         
    </div>
    
</template>
<script>
export default {
    
}
</script>
<style scoped>
    @import url('./footer.css');
</style>