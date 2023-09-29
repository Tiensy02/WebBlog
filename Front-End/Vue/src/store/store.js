import { createStore } from 'vuex'
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
    state: {
        langCode: getLangCodeFromLocalStorage(),
        postList: [],
        pagePost: 1,
        pageSizePost: 10,
        totalPost: 0,
        followList: [],
        pageFollow: 1,
        pageSizeFollow: 10,
        totalFollow: 0,
        isLoading: false,
        isPostList: false, // component ở main có phải postlisst không 
        isShowPaging: false, // có cho hiện phân trang không
        isSearch: false, // có đang trong trạng thái tìm kiếm không
        textSearch: "", // kí tự tìm kiếm
        isFolowedList: false, // có phải đang ở danh sách người dùng đang folow người dùng được chọn hay không
        isPostListOfUser: false, // có phải muốn lấy danh sách bài viết của 1 người dùng cụ thể hay không 
        userIDSelected: "", // id của người dùng đang được xem trang cá nhân
        userCurrentFollows:[], // danh sách những người mà user hiện thời đang follow
    },

    mutations: {
        // set ngôn ngữ
        setLangCode(state, newLangCode) {
            if (state.langCode != newLangCode) {
                localStorage.setItem("langCode", newLangCode)
                location.reload();
            }
        },
        // set trang và danh sách cho bài viết
        setPostList(state, newPostList) {
            state.postList = newPostList
        },
        setPagePostCurrent(state, newpageCurrent) {
            state.pagePost = newpageCurrent
        },
        setPagePostSize(state, newPageSize) {
            state.pageSizePost = newPageSize
        },
        setTotalPost(state, newTotalPost) {
            state.totalPost = newTotalPost
        },
        setLoading(state, newLoading) {
            state.isLoading = newLoading
        },
        // set trang và list cho follow
        setPageFollowCurrent(state, newPageCurrent) {
            state.pageFollow = newPageCurrent
        },
        setPageFollowSize(state, newPageSize) {
            state.pageSizeFollow = newPageSize
        },
        setFollowList(state, newFollows) {
            state.followList = newFollows
        },
        setTotalFollow(state, newTotalFollow) {
            state.totalFollow = newTotalFollow
        },
        // set trạng thái phân trang
        setIsShowPaging(state, statusPaging) {
            state.isShowPaging = statusPaging
        },
        setIsPostList(state, statusPostList) {
            state.isPostList = statusPostList
        },
        // set trạng thái tìm kiếm
        setIsSearch(state, newStatus) {
            state.isSearch = newStatus
        },
        //set ki tu tim kiem
        setTextSearch(state, newValue) {
            state.textSearch = newValue
        },
        // set trạng thái danh sách follow
        setIsFollowedList(state, newStatus) {
            state.isFolowedList = newStatus
        },
        //set trạng thái danh sách bài viết ( danh sách bài viết đang là danh sách của 1 user hay là tất cả bài viết)
        setIsPostListOfUser(state, newStatus) {
            state.isPostListOfUser = newStatus
        },
        //set id của người dùng đang được xem trang cá nhân
        setUserIDSelected(state, newID) {
            state.userIDSelected = newID
        },
        // set danh sách những người mà user hiện thời đang follow
        setUserCurrentFollows(state,newList){
            state.userCurrentFollows =  newList
        }
    },
    actions: {
        /**
         * @description hàm thực hiện lấy danh sách bài viết có phân trang
         * @param {Number} page số trang hiện tại
         * @param {Number} pageSise tổng số bài viết trên 1 trang
         */
        async getPostListAsync({ commit }, { page, pageSize }) {
            await new PostService().getPostListForUI(page, pageSize)
                .then(res => {
                    commit("setTotalPost", res.totalPost)
                    commit("setPostList", res.posts)
                    commit("setIsSearch", false)
                })
                .catch(err => {
                    console.error("lỗi ghi gọi api lấy danh sách bài viết" + err)
                })
        },
        /**
         * @description hàm thực hiện lấy danh sách bài viết của 1 user cụ thể
         * @param {*} param0 COMIT
         * @param {*} param1 các tham số
         */
        async getPostOfUser({ commit }, { id, page, pageSize }) {
            await new PostService().getPostOfUser(id, page, pageSize)
                .then(res => {
                    commit("setTotalPost", res.totalPost)
                    commit("setPostList", res.posts)
                    commit("setIsSearch", false)
                })
                .catch(err => {
                    console.log(err)
                })
        },
        /**
         * @description hàm thực hiện lấy thông tin cụ thể của 1 bài viết
         * @param {GUID} postId id của bài viết
         */
        async getPostAsync({ commit }, postId) {
            await new PostService().getPostById(postId)
                .then(res => {
                    commit("setPost", res)
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
        async getFollowedList({ commit }, { id, userCurrentID, page, pageSize }) {
            await new FollowService().getUserFollowed(id, userCurrentID, page, pageSize)
                .then(res => {
                    commit("setIsFollowedList", true)
                    commit("setFollowList", res.follows)
                    commit("setTotalFollow", res.totalFollow)
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
        async getFollowingList({ commit }, { id, userCurrentID, page, pageSize }) {
            await new FollowService().getUserFollowing(id, userCurrentID, page, pageSize)
                .then(res => {
                    commit("setIsFollowedList", false)
                    commit("setFollowList", res.follows)
                    commit("setTotalFollow", res.totalFollow)
                })
                .catch(err => {
                    console.log(err)
                })
        },
        /**
         * @description hàm thực hiện lấy danh sách bài viết phù hợp với ký tự tìm kiếm, có phân tran 
         * @param {Commit} commit 
         * @param {Params} textSearch ký tự tìm kiếm
         */
        async getPostForSearch({ commit }, { textSearch, page, pageSize }) {
            await new PostService().getPostByFilter(textSearch, page, pageSize)
                .then(res => {
                    commit("setPostList", res.posts)
                    commit("setTotalPost", res.totalPost)
                })
                .catch(err => {
                    console.log(err)
                })
        },
        /**
         * @description hàm thực hiện lấy danh sách những người mà user đang đăng nhập theo dõi
         * @param {*} param0 
         * @param {Object} {userCurrentID} id của user đang đăng nhập 
         */
        async getUserCurrentFollowsAsync({commit}, {userCurrentID }) {
            await new FollowService().getUserFollowed(userCurrentID,userCurrentID)
            .then(res => {
                commit('setUserCurrentFollows', res.follows)
            })
            .catch(err => {
                console.log(err);
            })
        }

    },


})
