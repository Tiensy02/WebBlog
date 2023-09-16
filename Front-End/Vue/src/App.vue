<script>
import TheMain from './layout/TheMain/theMain.vue';
import TheHeader from './layout/TheHeader/TheHeader.vue';
import theSidebar from './layout/theSidebar/theSidebar.vue';
import Footer from './layout/Footer/Footer.vue';
export default {
  name: 'app',
  data() {
    return {

    }
  },
  methods:{
    
  },
  components: {
    TheMain, TheHeader, theSidebar, Footer
  }
}
</script>

<template>
  <TheHeader></TheHeader>
  <div class="container">
  <TheMain></TheMain>
  </div>
  <Footer></Footer>
</template>

<style>
@import url('./style/main.css');
</style>
