import {createRouter, createWebHistory} from 'vue-router'
import WBPost from '../views/posts/post/WBPost.vue'
import PostService from '../service/post-service.js';
import PostList from '../views/posts/postList/PostList.vue'
import UserPage from '../views/userPage/UserPage.vue'
const router = createRouter({
    history:createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path:'/',
            component:PostList,
            name:'home',

        }
        ,{
            path:'/Post/:postID',
            component:WBPost,
            name:'post',
            props:true
        },
        {           
            path:'/User/:userID',
            component:UserPage,
            name:"user-page",
            props:true
        },
        {
            path:'/User/p/:userID',
            component:PostList,
            name:"post-of-user",
            props:true
        },
      ]
})
export default router; 