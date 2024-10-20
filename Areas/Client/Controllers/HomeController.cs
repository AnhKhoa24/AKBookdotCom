using AKBookdotCom.Contacts;
using AKBookdotCom.Models.Entities;
using AKBookdotCom.Models.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace AKBookdotCom.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ISachClient _service;
        private readonly QuanLySachContext _context;

        public HomeController(ISachClient service, QuanLySachContext context)
        {
            _service = service;
            _context = context;
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
