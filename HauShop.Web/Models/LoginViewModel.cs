using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HauShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Bạn cần nhập tài khoản")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Required]
        public bool RememberMe { set; get; }

    }
}