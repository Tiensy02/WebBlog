const WBEnum = {
    LANG_CODE:{
        VIET_NAM:"84",
        ENGLISH:"1"
    },
    MENU_USERS:{
        USER_PAGE:1, // vào trang cá nhân
        LOG_OUT:2, // đăng xuất tài khoản
        LOGIN:3, // đăng nhập tài khoản
        SIGN_UP:4 // đăng ký tài khoản
    },
    NOTIFICATION:{
        FOLLOW_NOTI: 1,// thông báo về follow
        RATING_NOTI:2, // thông báo về đánh giá bài viết   
        COMMENT_NOTI:3 // thông báo về comment
    },
    POST_ACTION:{
        DELETE: 1, // xoá bài viết
        FIX:2, // sửa bài viết
    },
    POST_fORM:{
        NEW_POST:1, // form thêm mới bài viết
        FIX_POST:2 //form sửa bài viết
    }

}
export default WBEnum