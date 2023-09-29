const vi = {
    common: {
      search: "Tìm kiếm",
      helpIconTooltip: "Giúp (F1)",
      closeIconTooltip: "Đóng (ESC)",
      totalPaging:"Số bản ghi/trang",
      total:"Tổng số",
      record:"Bản ghi",
      resource:'Tài nguyên',
      feedBack:'Phản hồi',
      help:'Giúp đỡ',
      newComment:"Bình luận mới",
      links:'Liên hệ',
      newPost:'Bài viết mới',
      emptyList:"Danh sách trống",
      gender: {
        male: "Nam",
        female: "Nữ",
        other: "Khác",
      },
      button: {
        edit: "Sửa",
        no: "Không",
        yes: "Có",
        add: "Thêm",
        store: "Lưu",
        storeAndAdd: "Lưu và thêm",
        close: "Đóng",
        cancel: "Hủy",
        continue: "Tiếp tục",
        rollBack: "Quay lại",
        dataInsert: "Nhập dữ liệu",
        implicitSampling: "Lấy mẫu ngầm định",
        allCategory: "Tất cả danh mục",
      },
      buttonTooltip: {
        store: "Lưu (Ctrl + S)",
        storeAndAdd: "Lưu và thêm (Ctrl + Shift + S)",
      },
    },
    validate: {
      nameFieldDefault: "Trường",
      paramPassToFunctionError: "Dữ liệu truyền vào hàm không hợp lệ.",
      maxLengthError: "Độ dài tối đa của <%s> là %d kí tự.",
      minLengthError: "Độ dài tối thiểu của <%s> là %d kí tự.",
      emptyError: "%s không được phép để trống.",
      dateNotGreaterThanToday: "%s không được lớn hơn ngày hiện tại.",
      formatError: "%s không đúng định dạng.",
      confirmError:"%s không trùng khớp",
      emailError:"Email không hợp lệ",
      passwordError:"Mật khẩu phải có cả ký tự, số và ít nhất 8 kí tự",
      confirmError:"%s không đúng",
      emailExits:"Email đã tồn tại",
      emailNotExits: "Email không tồn tại",
      loginError:"Tài khoản hoặc mật khẩu không chính xác",
      errorCommon:"Có lỗi xảy ra, vui lòng thử lại"

    },
  
    errorHandle: {
      serverError: {
        defaultError: "Lỗi đường truyền, vui lòng thử lại sau.",
      },
      loginError:"Bạn chưa đăng nhập, vui lòng thử lại sau",
  
      importExcel: { 
        selectSourceFile: {
        fileNotFound: "Chưa nhập file.",
        maxSizeError: "File quá lớn, không thể nhập khẩu.",
        formatError: "Tệp đính kèm không hợp lệ, chỉ chấp file .xlsx.",
        headerIndexGreaterThanZero: "Dòng tiêu đề phải lớn hơn 0.",
        sheetNotFound: "Không tìm thấy sheet.",
        },
        headerMerge: {
          headerRequiredNotMap:
            "Không thể tiếp tục nhập dữ liệu nếu các cột bắt buộc chưa được ghép với cột tương ứng trên tệp Excel. Vui lòng kiểm tra lại.",
        },
      },
    },
    userMenu:{ 
      logOut:"Đăng xuất",
      userPage:"Trang cá nhân",
      loginHeader:"Đăng nhập",
      singnUpHeader: "Đăng ký",
      forgotPassword:"Quên mật khẩu ?",
      signUpSuccess:"Tạo thành công",
      notifiComment:"Bình luận",
      notiFollow:"Theo dõi",
      notiRatting:"Đánh giá",
      notiNothing:"Không có thông báo mới",
      ratingName:"Điểm đánh giá",
      ratingReviews:"lượt"
    },
    userForm:{
      userName: "Tên tài khoản",
      password: "Mật khẩu",
      email:"Email",
      confirmPassword:"Nhập lại mật khẩu",
      notification:"Thông báo",
      postTitle:"Tiêu đề bài viết",
      postContent:"Nội dung bài viết",
      post:"Bài viết",
      followed:"Đang theo dõi",
      following:"Người theo dõi"
    },
    userAction:{
      writeResponse:"Viết một phản hồi",
      loginToComment:"Vui lòng đăng nhập để bình luận",
      postComment:"Đăng bình luận",
      reply:"Trả lời",
      discard:"Huỷ",
      post:"Đăng",
      showCommentChild:"Hiện thị %d trả lời",
      Rating:"Đánh giá",
      reviews:"lượt",
      ratingInserSuccess :"Cảm ơn bạn đã đánh giá",
      ratingUpdateSuccess:"Bạn đã chỉnh sửa đánh giá thành công",
      errorCommon:"Có gì đó sai",
      errorComment: "Thêm bình luận không thành công",
      delete:"Xóa",
      errorDeleteComment:"Xóa không thành công",
      fix:"Sửa",
      unFollow:"Bỏ theo dõi",
      deletePost:"Xoá bài viết",
      success:"thành công"

    },
    notification:{
      follow:"%s đã theo dõi bạn",
      commentPost:"%s đã bình luận bài viết của bạn",
      commentReply:"%s đã trả lời bình luận của bạn",
      ratting:"%s đã đánh giá bài viết của bạn"
    },
    warn:{
      deletePost:"Bài viết sau khi xoá sẽ không được khôi phục, bạn có muốn xoá bài viết"
    }
}  
export default vi