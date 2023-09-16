// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyC8x8Wyo1mDz5tPWAe88TzWQUahTj9daf4",
  authDomain: "webblog-25cda.firebaseapp.com",
  projectId: "webblog-25cda",
  storageBucket: "webblog-25cda.appspot.com",
  messagingSenderId: "445877325613",
  appId: "1:445877325613:web:acc7110c61c21cc16b4da4",
  measurementId: "G-4517G7X6V5"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
import {getStorage} from "firebase/storage"
const storage = getStorage(app)
export {storage}
