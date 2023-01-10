using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopWeb.UI.Models
{
    public class RegisterModel
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "yêu cầu nhập mật khẩu")]
        [StringLength(15,MinimumLength = 6 , ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password",ErrorMessage ="Xác nhận mật khẩu không đúng")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Họ tên")]
        public string Name { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "yêu cầu nhập email")]
        public string Email { get; set; }
        [Display(Name = "Đại chỉ")]
        public string Address { get; set; }
    }
}