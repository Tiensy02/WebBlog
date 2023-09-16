
import { createApp } from 'vue'
import App from './App.vue'
import WBIcon from './components/Icons/WBIcon.vue'
import contextMenu from './components/contextMenu/contextMenu.vue'
import WBButton from './components/button/WBButton.vue'
import textInput from './components/TextInput/textInput.vue'
import cloudImage from './components/cloudImage/cloudImage.vue'
import ToastMess from './components/ToastMess/ToastMess.vue'
import WBLoadding from './components/loadding/WBLoadding.vue'
import TextArea from './components/textArea/TextArea.vue'
import store from './store/store.js'
import i18n from './resourse/i18n.js'
import WBEnum from './enum/enum.js'
import router from './router/index.js'
import RatingStar from './components/ratingStar/RatingStar.vue'
import CommentWrite from './views/comments/CommentWrite.vue'
import WBTable from './components/table/WBTable.vue'
import DropdownList from './components/dropdownList/DropdownList.vue'
import WBPaging from './components/paging/WBPaging.vue'
const app = createApp(App)
app.component("wb-icon",WBIcon)
app.component("context-menu",contextMenu)
app.component("wb-button",WBButton)
app.component("text-input",textInput)
app.component("cloud-image",cloudImage)
app.component("toast-mess",ToastMess)
app.component("wb-loadding",WBLoadding)
app.component("text-area", TextArea)
app.component("rating-star",RatingStar)
app.component("comment-write",CommentWrite)
app.component("wb-table",WBTable)
app.component("wb-paging",WBPaging)
app.component("dropdown-list",DropdownList)
app.use(store)
app.use(router)
app.use(i18n)
app.config.globalProperties.$_WBEnum = WBEnum;
app.mount('#app')
