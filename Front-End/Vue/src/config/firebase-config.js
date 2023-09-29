// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyCesCuWvUZWm5YfwzPmN87bj2Bmhupe32Y",
  authDomain: "webblog-4160a.firebaseapp.com",
  projectId: "webblog-4160a",
  storageBucket: "webblog-4160a.appspot.com",
  messagingSenderId: "801398686781",
  appId: "1:801398686781:web:02dfce91b741743a181654",
  measurementId: "G-KLZ9S7MMSD"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);
import {getStorage} from "firebase/storage"
const storage = getStorage(app)
export {storage}
