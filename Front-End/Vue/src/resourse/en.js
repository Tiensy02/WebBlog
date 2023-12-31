const en = {
    common: {
      search: "Search",
      helpIconTooltip: "Help (F1)",
      closeIconTooltip: "Close (ESC)",
      totalPaging:"records/page",
      total:"Total",
      record:"Records",
      resource:'Resources',
      feedBack:'FeedBack',
      newPost:'New Post',
      emptyList:"List Empty",
      help:'Help',
      links:'Links',
      newComment:"New Comments",
      gender: {
        male: "Male",
        female: "Female",
        other: "Other",
      },
      button: {
        edit: "Edit",
        no: "No",
        yes: "Yes",
        add: "Add",
        store: "Save",
        storeAndAdd: "Save and Add",
        close: "Close",
        cancel: "Cancel",
        continue: "Continue",
        rollBack: "Roll Back",
        dataInsert: "Insert Data",
        implicitSampling: "Implicit Sampling",
        allCategory: "All Categories",
      },
      buttonTooltip: {
        store: "Save (Ctrl + S)",
        storeAndAdd: "Save and Add (Ctrl + Shift + S)",
      },
    },
  
    validate: {
      nameFieldDefault: "Field",
      paramPassToFunctionError: "Invalid data passed to the function.",
      maxLengthError: "The maximum length of <%s> is %d characters.",
      minLengthError: "The minimum length of <%s> is %d characters.",
      emptyError: "%s cannot be empty.",
      dateNotGreaterThanToday: "%s cannot be greaterthan the current date.",
      formatError: "%s is not in the correct format.",
      emailError:"Invalid email",
      passwordError:"Password must contain letters, numbers and at least 8 characters",
      confirmError:"%s incorrect",
      emailExits:"Email adready exists",
      emailNotExits: "Email không tồn tại",
      loginError:"Tài khoản hoặc mật khẩu không chính xác",
      errorCommon:"An error occurred, please try again"
      
    },
  
    errorHandle: {
      serverError: {
        defaultError: "Network error, please try again later.",
      },
      loginError:"You are not logged in, please try again later",
      importExcel: {
        selectSourceFile: {
          fileNotFound: "File not selected.",
          maxSizeError: "File is too large, cannot be imported.",
          formatError: "Invalid attachment, only .xlsx files are allowed.",
          headerIndexGreaterThanZero: "The header row must be greater than 0.",
          sheetNotFound: "Sheet not found.",
        },
  
        headerMerge: {
          headerRequiredNotMap:
            "Cannot continue data import if required columns are not mapped with corresponding columns in the Excel file. Please check again.",
        },
      },
    },
    userMenu:{
      logOut:"Sign out",
      userPage:"Profile",
      loginHeader:"Login",
      singnUpHeader: "Sign Up",
      forgotPassword:"Forgot Password ?",
      signUpSuccess:"Success",
      notifiComment:"Comment",
      notiFollow:"Follow",
      notiRatting:"Ratting",
      notiNothing:"No new announcements",
      ratingName:"Rating Core",
      ratingReviews:"turn"

    },
    userForm:{
      userName: "Username", 
      password: "Password",
      email:"Email",
      confirmPassword:"Confirm Password",
      notification:"Notifications",
      postTitle:"Post Title",
      postContent:"Post Content",
      post:"Post",
      followed:"Followed",
      following:"Following"
    },
    userAction:{
      writeResponse:"Write a responsive",
      loginToComment:"Please login to comment",
      postComment:"Post comment",
      reply:"Reply",
      discard:"Discard",
      post:"Post",
      showCommentChild:"Showing %d answers",
      Rating:"Rating",
      reviews:"reviews",
      ratingInserSuccess :"Thank you for rating",
      ratingUpdateSuccess:"You have successfully edited your review",
      errorCommon:"Something wrong",
      errorComment: "Adding comment failed",
      delete:"Delete",
      errorDeleteComment:"Delete failed",
      fix:"Fix",
      unFollow:"unFollow",
      deletePost:"Delete Post",
      success:"Success"
    },
    notification:{
      follow:"%s has been following you",
      commentPost:"%s commented on your post",
      commentReply:"%s has replied to your comment",
      ratting:"%shas rated your post"
    },
    warn:{
      deletePost:"Once deleted, posts will not be restored. Do you want to delete the post?"
    }
}
export default en