﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public class CommentModel
    {
        public string CommentContent { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public Guid CommentID { get; set; }
        public int NumberCommentChild { get; set; }
        public string UserAvatar { get; set; }
        public Guid UserID { get; set; }

    }
}
