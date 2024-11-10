using AKBookdotCom.Contacts;
using AKBookdotCom.Models.Entities;
using AKBookdotCom.Models.Support;
using AKBookdotCom.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AKBookdotCom.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ISachClient _service;
        private readonly QuanLySachContext _context;
        private readonly IVNpayService _vnpservie;

        public HomeController(ISachClient service, QuanLySachContext context, IVNpayService vnpservie)
        {
            _service = service;
            _context = context;
            _vnpservie = vnpservie;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ChuDes = await _service.getChuDe();
            ViewBag.NhaXuatBans = await _service.getNhaxuatban();
            ViewBag.Title = "Trang chủ";
            ViewBag.Link = 1;
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            
            var accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
            ViewBag.CartList = await _context.Carts
                .Include(i=>i.MaSachNavigation)
                .Where(x=>x.MaKh == accountId)
                .ToListAsync();
            ViewBag.ChuDes = await _service.getChuDe();
            ViewBag.NhaXuatBans = await _service.getNhaxuatban();
            ViewBag.Title = "Trang chủ";
            ViewBag.Link = 1;
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
           
            int accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
            ViewBag.User = await _context.Khachhangs.FindAsync(accountId);
            ViewBag.CartList = await _context.Carts
                .Include(i => i.MaSachNavigation)
                .Where(x => x.MaKh == accountId)
                .ToListAsync();
            ViewBag.ChuDes = await _service.getChuDe();
            ViewBag.NhaXuatBans = await _service.getNhaxuatban();
            ViewBag.Title = "Thanh toán";
            ViewBag.Link = 3;
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeSL(int idsach, int sl)
        {
            int accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
            var getCart = await _context.Carts.FirstOrDefaultAsync(x=>x.MaKh == accountId && x.MaSach == idsach);
            getCart!.Sl = sl;
            getCart.Tongtien = getCart.Gia * sl;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteCart(int idsach)
        {
            int accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
            var getCart = await _context.Carts.FirstOrDefaultAsync(x => x.MaKh == accountId && x.MaSach == idsach);
            _context.Remove(getCart!);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        public async Task<IActionResult> DonHang()
        {
            ViewBag.ChuDes = await _service.getChuDe();
            ViewBag.NhaXuatBans = await _service.getNhaxuatban();
            int accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
            var data = await _context.Dondathangs
              .Where(x=>x.MaKh == accountId)
              .Select(t => new
              {
                  t.MaDonHang,
                  Ngaydat = t.Ngaydat.HasValue
          ? t.Ngaydat.Value.ToDateTime(TimeOnly.MinValue).ToString("dd-MM-yyyy")
          : null,
                  Ngaygiao = t.Ngaygiao.HasValue
          ? t.Ngaygiao.Value.ToDateTime(TimeOnly.MinValue).ToString("dd-MM-yyyy")
          : null,
                  t.Tinhtranggiaohang,
                  sanpham = t.Chitietdondathangs.Select(n => new
                  {
                      n.MasachNavigation!.Tensach,
                      n.Dongia,
                      n.Soluong,
                      ThanhTien = n.Dongia * n.Soluong,
                  }).ToList(),
              })
              .OrderByDescending(o => o.MaDonHang)
              .ToListAsync();
            ViewBag.DonHangs = data;
            ViewBag.Title = "Đơn hàng";
            ViewBag.Link = 2;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RenderCart()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
                var cart = await _context.Carts
                    .Include(i => i.MaSachNavigation)
                    .Where(x => x.MaKh == accountId)
                    .ToListAsync();
                if (cart.Count < 1)
                {
                    ViewBag.Iscart = false;
                }
                else
                {
                    ViewBag.Iscart = true;
                }   
                ViewBag.CartList = cart;
                return PartialView();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CountInCart()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
                var cart = await _context.Carts
                    .Include(i => i.MaSachNavigation)
                    .Where(x => x.MaKh == accountId)
                    .ToListAsync();
                return Ok(new
                {
                    soluong = cart.Count,
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Order(string checkoutType)
        {
            int accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var cart = await _context.Carts.Where(x => x.MaKh == accountId).ToListAsync();
                if (!cart.Any())
                {
                    return BadRequest(new
                    {
                        message="Giỏ hàng trống!"
                    });
                }

                var dondathang = new Dondathang
                {
                    Ngaydat = DateOnly.FromDateTime(DateTime.Now),
                    Ngaygiao = DateOnly.FromDateTime(DateTime.Now).AddDays(3),
                    Tinhtranggiaohang = "Đã đặt hàng",
                    MaKh = accountId
                };
                await _context.Dondathangs.AddAsync(dondathang);
                await _context.SaveChangesAsync(); 

                var Ds = cart.Select(item => new Chitietdondathang
                {
                    MaDonHang = dondathang.MaDonHang,
                    Masach = item.MaSach,
                    Soluong = item.Sl,
                    Dongia = item.Gia
                    
                }).ToList();

                decimal tongtien = 0;
                foreach(var d in cart)
                {
                    tongtien += d.Tongtien??0;
                }    

                await _context.Chitietdondathangs.AddRangeAsync(Ds);
                _context.Carts.RemoveRange(cart);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                if (checkoutType == "VNPAY")
                {
                    var vnPayModel = new VnPaymentRequestModel
                    {
                        Amount = Convert.ToDouble(tongtien),
                        CreatedDate = DateTime.Now,
                        Description = $"HuynhANH KHOA giao dich",
                        FullName = "khoa",
                        OrderId = new Random().Next(1000, 100000)
                    };
                    return Redirect(_vnpservie.CreatePaymentUrl(HttpContext, vnPayModel));
                }

                return Ok(new
                {
                    message = "Đặt hàng thành công!"
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> BackPayment()
        {
            var response = _vnpservie.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                ViewBag.Message = "Thanh toán thất bại!";
            }
            else if (response.VnPayResponseCode == "00")
            {
                ViewBag.Message = "Thanh toán thành công!";
            }
            ViewBag.ChuDes = await _service.getChuDe();
            ViewBag.NhaXuatBans = await _service.getNhaxuatban();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detaiil(long idsach)
        {
            //var id = idsach == 0 ? 1 : idsach;
            ViewBag.ChuDes = await _service.getChuDe();
            ViewBag.NhaXuatBans = await _service.getNhaxuatban();
            ViewBag.Sach = await _context.Saches.FirstOrDefaultAsync(x=>x.Masach == idsach);
            ViewBag.Title = ViewBag.Sach.Tensach;
            ViewBag.Link = 2;
            ViewBag.SachList = await _context.Saches.Where(x=>x.Masach != idsach).Take(5).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetSach(PayloadIndex model)
        {
            ViewBag.Saches = await _service.getSaches(model);
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> GetMaxTrang(PayloadIndex model)
        {
            string search = model.Search ?? "";
            int totalRecords = await _context.Saches
                .Where(x=>(x.Tensach!.Contains(search)||search == "")
                && ((x.MaCd == model.MaCD || model.MaCD == 0) && (x.MaNxb == model.MaNXB || model.MaNXB == 0))
                )
                .CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)12);
            return Ok(new { totalPages });
        }
        [HttpPost]
        public async Task<IActionResult> getSub(string search)
        {
            return Ok(await _service.getSub(search));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> addToCart(int idsach, int sl)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                //var accountId = User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value;
                var accountId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value);
                var sach = await _context.Saches.FindAsync(idsach);
                var check = await _context.Carts.FirstOrDefaultAsync(x=>x.MaKh == accountId&&x.MaSach == idsach);
                if (check != null)
                {
                    check.MaSach = idsach;
                    check.MaKh = accountId;
                    check.Sl += sl;
                    check.Gia = sach!.Giaban;
                    check.Tongtien = check.Sl * sach.Giaban;
                } else
                {
                    Cart newCart = new Cart();
                    newCart.MaSach = idsach;
                    newCart.MaKh = accountId;
                    newCart.Sl = sl;
                    newCart.Gia = sach!.Giaban;
                    newCart.Tongtien = sl * sach.Giaban;
                    await _context.Carts.AddAsync(newCart);
                }    
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    status = 200,
                    message = "Thêm thành công"
                });
            }
            else
            {
                return Ok(new
                {
                    status = 500,
                    message = "Chưa đăng nhập"
                });
            }
        }
    }
}
