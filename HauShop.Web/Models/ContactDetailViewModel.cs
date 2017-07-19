using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HauShop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { set; get; }
        [Required(ErrorMessage ="Tên không được trống")]
        [MaxLength(50, ErrorMessage ="Tên không vượt quá 50 ký tự")]
        public string Name { set; get; }
        [MaxLength(250, ErrorMessage = "Tên không vượt quá 250 ký tự")]
        public string Phone { set; get; }
        [MaxLength(250, ErrorMessage = "Phone không vượt quá 250 ký tự")]
        public string Email { set; get; }
        [MaxLength(250, ErrorMessage = "Email không vượt quá 250 ký tự")]
        public string Website { set; get; }
        [MaxLength(250, ErrorMessage = "Website không vượt quá 250 ký tự")]
        public string Address { set; get; }
        [MaxLength(250, ErrorMessage = "Address không vượt quá 250 ký tự")]
        public string Other { set; get; }
        public double? Lat { set; get; }
        public double? Lng { set; get; }
        public bool Status { set; get; }
    }
}