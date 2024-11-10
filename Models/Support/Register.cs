using System.ComponentModel.DataAnnotations;

namespace AKBookdotCom.Models.Support
{
    public class Register
    {

        [Required(ErrorMessage = "Họ tên không được bỏ trống")]
        //[StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string HoTen { get; set; } = null!;

        [Required(ErrorMessage = "Tên tài khoản không được bỏ trống")]
        public string TaiKhoan { get; set; } = null!;

        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có độ dài ít nhất 3 ký tự", MinimumLength = 3)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string RepeatPassword { get; set; } = null!;

        public string? DiaChiKH { get; set; }
        public string? DienThoaiKH { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateOnly Ngaysinh { get; set; }

    }
}
