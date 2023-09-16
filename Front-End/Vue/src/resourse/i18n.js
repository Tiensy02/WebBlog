import { createI18n } from "vue-i18n";
import WBResource from "./resourse.js";
import store from '../store/store.js';

const i18n = createI18n({
  locale: store.state.langCode,
  fallbackLocale: "vi",
  messages: WBResource,
});

export default i18n;


