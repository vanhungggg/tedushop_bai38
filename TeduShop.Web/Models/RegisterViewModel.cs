using System.ComponentModel.DataAnnotations;

namespace TeduShop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn phải nhập họ tên.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập email.")]
        [EmailAddress(ErrorMessage = "địa chỉ email không đúng")]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập số điện thoại.")]
        public string PhoneNumber { get; set; }
    }
}