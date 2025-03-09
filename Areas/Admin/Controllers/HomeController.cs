using AKBookdotCom.Areas.Admin.Contacts;
using AKBookdotCom.Contacts;
using AKBookdotCom.Models.Entities;
using AKBookdotCom.Models.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace AKBookdotCom.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Policy = "AdminOnly")]
    public class HomeController : Controller
    {
        private readonly IQuanLySach _service;
        private readonly QuanLySachContext _context;

        public HomeController(IQuanLySach service, QuanLySachContext context)
        {
            _service = service;
            _context = context;
        }
        public async Task<IActionResult> ThongKeDonHang(string ngaybatdau, string ngayketthuc)
        {
            return Ok(await _context.GetThongKeDonHangAsync(ngaybatdau, ngayketthuc));
        }
        public IActionResult Index()
        { 
            return View(); 
        }
        public IActionResult QuanLySach()
        {
            return View();
        }
        public IActionResult DonHang()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> getDonHang(int trang, int pagesize, string search)
        {
            return Ok(await _service.getDonHang(trang, pagesize, search));
        }
        [HttpPost]
        public async Task<IActionResult> Sach(int trang, int pagesize, string search)
        {
            return Ok(await _service.SachService(trang, pagesize, search));
        }
        [HttpPost]
        public async Task<IActionResult> GetTacGia(string search)
        {
            var tim = search == null ? "" : search.Trim();
            var tacgiacs = await _context.Tacgia.
                Where(x => (x.TenTg!.Contains(tim) || tim == ""))
                .ToListAsync();
            return Ok(tacgiacs);
        }
        [HttpPost]
        public async Task<IActionResult> GetNhaXuatBan(string search)
        {
            var tim = search == null ? "" : search.Trim();
            var tacgiacs = await _context.Nhaxuatbans.
                Where(x => (x.TenNxb!.Contains(tim) || tim == ""))
                .ToListAsync();
            return Ok(tacgiacs);
        }
        [HttpPost]
        public async Task<IActionResult> GetChuDe(string search)
        {
            var tim = search == null ? "" : search.Trim();
            var chudes = await _context.Chudes.
                Where(x => (x.TenChuDe!.Contains(tim) || tim == ""))
                .ToListAsync();
            return Ok(chudes);
        }
        [HttpPost]
        public async Task<IActionResult> AddSach([FromForm] Addbook model)
        {
            Sach themSach = new Sach();
            if (model.MaSach != 0)
            {
                var sach = await _context.Saches.FirstOrDefaultAsync(x=>x.Masach == model.MaSach);
                if (sach != null) themSach = sach;
            }
            if (model.file != null && model.file.Length != 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                var timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(model.file.FileName);
                var fileExtension = Path.GetExtension(model.file.FileName);
                var newFileName = $"{timeStamp}_{fileNameWithoutExtension}{fileExtension}";
                var filePath = Path.Combine(uploads, newFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.file.CopyToAsync(stream);
                }
                themSach.Anhbia = newFileName;
            }
            
            themSach.Tensach = model.TenSach;
            themSach.Giaban = model.GiaBan;
            themSach.Mota = model.MoTa;
            themSach.Ngaycapnhat = DateOnly.FromDateTime(DateTime.Now);
            themSach.Soluongton = model.SLTon;
            themSach.MaCd = model.MaCD;
            themSach.MaNxb = model.MaNXB;


            if (model.MaSach == 0)
            {
                await _context.Saches.AddAsync(themSach);
                await _context.SaveChangesAsync();
            }
            if(model.MaSach != 0)
            {
                var vietSac = await _context.Vietsacs.Where(x=>x.Masach == themSach.Masach).ToListAsync();
                if(vietSac.Any())
                {
                    _context.Vietsacs.RemoveRange(vietSac);
                    await _context.SaveChangesAsync();
                }    
            }
         
            if(model.TacGias != null)
            {
                List<Vietsac> vs = new List<Vietsac>();
                foreach (var item in model.TacGias!)
                {
                    Vietsac taomoi = new Vietsac();
                    taomoi.Masach = themSach.Masach;
                    taomoi.Vaitro = "Tác giả";
                    taomoi.Vitri = "Hi hi";
                    taomoi.MaTg = item;
                    vs.Add(taomoi);
                }
                await _context.Vietsacs.AddRangeAsync(vs);
            }    
            
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult>getDataEdit(int idsach)
        {
            var sachs = await _context.Saches
                .Where(x=>x.Masach == idsach)
                .Select(t => new
                {
                    t.Masach,
                    t.Tensach,
                    t.Anhbia,
                    t.Mota,
                    t.Soluongton,
                    t.Giaban,
                    t.MaCdNavigation!.MaCd,
                    t.MaCdNavigation!.TenChuDe,
                    t.MaNxbNavigation!.TenNxb,
                    t.MaNxb,
                    dstacgia = t.Vietsacs.Select(x => new
                    {
                        x.MaTgNavigation.TenTg,
                        x.MaTg
                    }).ToList(),
                })
                .FirstAsync();
            return Ok(sachs);
        }
    }

}
