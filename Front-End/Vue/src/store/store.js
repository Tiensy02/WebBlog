import {createStore} from 'vuex'
import PostService from '../service/post-service.js'
import FollowService from '../service/follow-service.js'



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
        postList:[],
        pagePost :1,
        pageSizePost:10,
        totalPost:0,
        followList:[],
        pageFollow:1,
        pageSizeFollow:10,
        totalFollow:0,
        isLoading:false,
        isPostList:false, // component ở main có phải postlisst không 
        isShowPaging: false, // có cho hiện phân trang không
    },

    mutations:{
        // set ngôn ngữ
        setLangCode(state, newLangCode){
            if(state.langCode != newLangCode ) {
                localStorage.setItem("langCode",newLangCode)
                location.reload();
            }
        },
        // set trang và danh sách cho bài viết
        setPostList(state,newPostList){
            state.postList = newPostList
        },
        setPagePostCurrent(state,newpageCurrent) {
            state.pagePost = newpageCurrent
        },
        setPagePostSize(state, newPageSize){
            state.pageSizePost = newPageSize
        },
        setTotalPost(state,newTotalPost) {
            state.totalPost = newTotalPost
        },
        setLoading(state,newLoading){
            state.isLoading = newLoading
        },
        // set trang và list cho follow
        setPageFollowCurrent(state, newPageCurrent){
            state.pageFollow = newPageCurrent
        },
        setPageFollowSize(state,newPageSize){
            state.pageSizeFollow = newPageSize
        },
        setFollowList(state, newFollows) {
            state.followList = newFollows
        },
        setTotalFollow(state,newTotalFollow) {
            state.totalFollow = newTotalFollow
        },
        // set trạng thái phân trang
        setIsShowPaging(state,statusPaging) {
            state.isShowPaging = statusPaging
        },
        setIsPostList(state, statusPostList) {
            state.isPostList = statusPostList
        }
    },
    actions:{
        /**
         * @description hàm thực hiện lấy danh sách bài viết có phân trang
         * @param {Number} page số trang hiện tại
         * @param {Number} pageSise tổng số bài viết trên 1 trang
         */
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
        /**
         * @description hàm thực hiện lấy thông tin cụ thể của 1 bài viết
         * @param {GUID} postId id của bài viết
         */
        async getPostAsync({commit},postId) {
            await new PostService().getPostById(postId)
            .then(res => {
                commit("setPost",res)
            })
            .catch(err => {
                console.log(err)
            })
        },
        /**
         * @description hàm thực hiện lấy danh sách người dùng mà người user có id  đang follow 
         * @param {Number} page trang hiện tại
         * @param {Number} pageSize tổng số trang trên bản ghi
         */
        async getFollowedList({commit}, {id,page, pageSize}) {
            await new FollowService().getUserFollowed(id,page,pageSize)
            .then(res => {
                commit("setFollowList",res.follows)
                commit("setTotalFollow",res.totalFollow)
            })
            .catch(err => {
                console.log(err)
            })
        },
        /**
         * @description hàm thực hiện lấy danh sách người dùng theo dõi người dùng có id
         * @param {Number} page trang hiện tại
         * @param {Number} pageSize
         */
        async getFollowingList({commit}, {id,page, pageSize}) {
            await new FollowService().getUserFollowing(id,page,pageSize)
            .then(res => {
                commit("setFollowList",res.follows)
                commit("setTotalFollow",res.totalFollow)
            })
            .catch(err => {
                console.log(err)
            })
        }
    },

    
})
