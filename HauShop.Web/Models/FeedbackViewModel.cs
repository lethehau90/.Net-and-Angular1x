using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HauShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { set; get; }
        [MaxLength(250, ErrorMessage ="Tên không được quá 250 ký tự")]
        [Required(ErrorMessage ="Tên phải được nhập")]
        public string Name { set; get; }
        [MaxLength(250,ErrorMessage = "Email không được quá 250 ký tự")]
        [Required(ErrorMessage = "Email phải được nhập")]
        public string Email { set; get; }
        [MaxLength(500, ErrorMessage = "Tin nhắn không được quá 500 ký tự")]
        public string Message { set; get; }
        public DateTime CreateDate { set; get; }
        [Required(ErrorMessage ="phải nhập trạng thái")]
        public bool Status { set; get; }

        public ContactDetailViewModel ContactDetail { set; get; }
    }
}