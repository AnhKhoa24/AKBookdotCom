using System.ComponentModel.DataAnnotations;

namespace AKBookdotCom.Models.Support
{
    public class Login
    {
        [Required(ErrorMessage = "Tài khoản không được bỏ trống")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có độ dài ít nhất 6 ký tự", MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }
}
