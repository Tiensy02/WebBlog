<script>
import TheMain from './layout/TheMain/theMain.vue';
import TheHeader from './layout/TheHeader/TheHeader.vue';
import theSidebar from './layout/theSidebar/theSidebar.vue';
import Footer from './layout/Footer/Footer.vue';
export default {
  name: 'app',
  
  data() {
    return {
      user :JSON.parse(localStorage.getItem("user"))
    }
  },
  methods: {
    handleFileUpload(event) {
      const file = event.target.files[0];

      // Tạo một đối tượng FileReader
      const reader = new FileReader();

      // Xử lý sự kiện khi FileReader đọc xong file
      reader.onload = function () {
        const base64Data = reader.result;
        callAPI(event.target.files[0])

        // Xử lý mã base64 ở đây
      };

      // Đọc file dưới dạng ArrayBuffer
      reader.readAsDataURL(file);
    }
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
    <theSidebar></theSidebar>
  </div>
  <Footer></Footer>
</template>

<style>
@import url('./style/main.css');
</style>
