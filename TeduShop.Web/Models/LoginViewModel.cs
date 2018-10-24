using System.ComponentModel.DataAnnotations;

namespace TeduShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập Username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}