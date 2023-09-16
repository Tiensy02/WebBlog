import {createStore} from 'vuex'
import PostService from '../service/post-service.js'



/**
 * @description hàm thực hiện lấy langCode trong localStrorage
 * @returns langCode
 */
function getLangCodeFromLocalStorage() {
    let langCode = localStorage.getItem("langCode");
    if (!langCode) {
      localStorage.setItem("langCode", "vi");
      
      langCode = localStorage.getItem("langCode");
    }
  
    return langCode;
}

export default createStore({
    state:{
        langCode:getLangCodeFromLocalStorage(),
        PostList:[],
        pagePost :1,
        pageSizePost:100,
        totalPost:0,
        pageComment:1,
        pageSizeComment:100,
        post:{},
        isLoading:false
    },

    mutations:{
        setLangCode(state, newLangCode){
            if(state.langCode != newLangCode ) {
                localStorage.setItem("langCode",newLangCode)
                location.reload();
            }
        },
        setPostList(state,newPostList){
            state.PostList = newPostList
        },
        setPageCurrent(state,newpageCurrent) {
            state.pagePost = newpageCurrent
        },
        setPageSize(state, newPageSize){
            state.pageSizePost = newPageSize
        },
        setTotalPost(state,newTotalPost) {
            state.totalPost = newTotalPost
        },
        setCommentRootForPost(state,newCommentRoot){
            state.commentRootsForPost = newCommentRoot
        },
        setPageComment(state,newPageComment) {
            state.pageComment = newPageComment
        },
        setPageSizeComment(state,newPageSizeComment) {
            state.pageSizeComment = newPageSizeComment
        },
        setPost(state,newPost){
            state.post = newPost
        },
        setLoading(state,newLoading){
            state.isLoading = newLoading
        }
    },
    actions:{
        async getPostListAsync({commit}, {page, pageSize}){
            await new PostService().getPostListForUI(page,pageSize)
            .then(res=> {
                commit("setTotalPost", res.totalPost)
                commit("setPostList", res.posts)
            })
            .catch(err=>{
                console.error("lỗi ghi gọi api lấy danh sách bài viết" + err)
            })
        },
        async getPostAsync({commit},postId) {
            await new PostService().getPostById(postId)
            .then(res => {
                commit("setPost",res)
            })
            .catch(err => {
                console.log(err)
            })
        }
        
    },
    
})
