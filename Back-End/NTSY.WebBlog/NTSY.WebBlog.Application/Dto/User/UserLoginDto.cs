﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set;}
        [Required]
        public string UserPassword { get; set;}
    }
}
