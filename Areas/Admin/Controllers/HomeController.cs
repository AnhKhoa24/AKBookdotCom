using AKBookdotCom.Areas.Admin.Contacts;
using AKBookdotCom.Contacts;
using AKBookdotCom.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AKBookdotCom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class HomeController : Controller
    {
        private readonly IQuanLySach _service;
        private readonly QuanLySachContext _context;

        public HomeController(IQuanLySach service, QuanLySachContext context)
        {
            _service = service;
            _context = context;
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
    }

}
