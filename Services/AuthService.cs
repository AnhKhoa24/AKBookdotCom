using AKBookdotCom.Contacts;
using AKBookdotCom.Models.Entities;
using AKBookdotCom.Models.Support;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static AKBookdotCom.Models.Support.Response;

namespace AKBookdotCom.Services
{
    public class AuthService : IAuthService
    {
        private readonly QuanLySachContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(QuanLySachContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<LoginResponse> Login(Login model)
        {
            var check =await _context.Khachhangs.Include(x=>x.IdQuyenNavigation).FirstOrDefaultAsync(x=>(x.Taikhoan == model.Email|| x.Email == model.Email)
            && x.Matkhau == Helper.HashPassword(model.Password));
            if (check == null)
            {
                return new LoginResponse(false, null, "Tài khoản hoặc mật khẩu không chính xác");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, check.HoTen!),
                new Claim(ClaimTypes.Role, check.IdQuyenNavigation!.TenQuyen!),
                new Claim(ClaimTypes.Email, check.Email!),
                new Claim(ClaimTypes.MobilePhone, check.DienthoaiKh??""),
                new Claim(ClaimTypes.StreetAddress, check.DiachiKh??""),
                new Claim("AccountId", check.MaKh.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return new LoginResponse(true, check.IdQuyenNavigation.TenQuyen, "Đăng nhập thành công!");
        }

        public Task<GeneralResponse> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponse> Register(Register model)
        {
            var check = _context.Khachhangs.FirstOrDefault(x=>x.Taikhoan == model.TaiKhoan||x.Email==model.Email);
            if (check != null)
            {
                return new GeneralResponse(false,"Tài khoản đã tồn tại!");
            }
            Khachhang khachhang = new Khachhang();
            khachhang.HoTen = model.HoTen;
            khachhang.Taikhoan = model.TaiKhoan;
            khachhang.Matkhau = Helper.HashPassword(model.Password);
            khachhang.Email = model.Email;
            khachhang.DiachiKh = model.DiaChiKH;
            khachhang.DienthoaiKh = model.DienThoaiKH;
            khachhang.Ngaysinh = model.Ngaysinh;
            khachhang.IdQuyen = 2;
            try
            {
                await _context.Khachhangs.AddAsync(khachhang);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Đăng ký thành công");
            }
            catch (Exception ex) {
                return new GeneralResponse(false, ex.Message);
            }
        }
    }
}