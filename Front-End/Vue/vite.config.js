import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  build: {
    rollupOptions: {
      // Sử dụng hàm transform có sẵn của Rollup để thay thế biến đặc trưng
      output: {
        manualChunks: undefined, // Không dùng manualChunks
        plugins: [
          {
            name: 'replace-vue-i18n-flags',
            transform(code) {
              // Thay thế biến đặc trưng bằng giá trị boolean cụ thể
              return code.replace(/__VUE_I18N_FULL_INSTALL__ = .+/, '__VUE_I18N_FULL_INSTALL__ = false;')
                         .replace(/__VUE_I18N_LEGACY_API__ = .+/, '__VUE_I18N_LEGACY_API__ = false;')
                         .replace(/__VUE_I18N_PROD_DEVTOOLS__ = .+/, '__VUE_I18N_PROD_DEVTOOLS__ = false;');
            },
          },
        ],
      },
    },
  }
})
