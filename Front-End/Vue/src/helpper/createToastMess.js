import { createApp, h } from 'vue';
import ToastMess from '../components/ToastMess/ToastMess.vue';
const createToast = (contentMess, typeToastMess = 'success') => {
  const toastApp = createApp({
    render() {
      return h(ToastMess, {
        contentMess: contentMess,
        isAppear: true,
        typeToastMess: typeToastMess,
        onHiddenToastMess: () => {
          clearTimeout(timeoutId); // Xoá timeout nếu tắt toast trước khi hết thời gian
          toastApp.unmount();
          const toastContainer = document.getElementById('toast-container');
          if (toastContainer && toastContainer.parentNode) {
            toastContainer.parentNode.removeChild(toastContainer);
          }
        },
      });
    },
  });

  const toastContainer = document.createElement('div');
  toastContainer.id = 'toast-container';
  document.body.appendChild(toastContainer);

  const timeoutId = setTimeout(() => {
    toastApp.unmount();
    toastContainer.parentNode.removeChild(toastContainer);
  }, 4000); // Xoá toastContainer sau 4 giây (4000ms)

  toastApp.mount(toastContainer);
};

export default createToast;