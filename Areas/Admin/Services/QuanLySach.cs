using AKBookdotCom.Areas.Admin.Contacts;
using AKBookdotCom.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AKBookdotCom.Areas.Admin.Services
{
    public class QuanLySach : IQuanLySach
    {
        private readonly QuanLySachContext _context;

        public QuanLySach(QuanLySachContext context)
        {
            _context = context;
        }

        public async Task<dynamic> getDonHang(int trang, int pagesize, string search)
        {
            var data = await _context.Dondathangs
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
                   t.MaKhNavigation!.HoTen,
                   t.MaKhNavigation.DiachiKh,
                   t.MaKhNavigation.DienthoaiKh,
                   sanpham = t.Chitietdondathangs.Select(n => new
                   {
                       n.MasachNavigation!.Tensach,
                       n.Dongia,
                       n.Soluong,
                       ThanhTien = n.Dongia * n.Soluong,
                   }).ToList(),
               })
               .Skip((trang - 1) * pagesize)
               .Take(pagesize)
               .ToListAsync();
            int totalRecords = await _context.Dondathangs.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pagesize);
            return new
            {
                trang = trang,
                pagesize = pagesize,
                tongtrang = totalPages,
                data = data
            };
        }

        public async Task<dynamic> SachService(int trang, int pagesize, string search)
        {
            var paginatedData = await _context.Saches
                 //.Where(x=>(x.Tensach!.Contains(search)|| search ==""))
                 .Skip((trang - 1) * pagesize)
                 .Take(pagesize)
                 .ToListAsync();
            int totalRecords = await _context.Saches.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pagesize);
            return new
            {
                trang = trang,
                pagesize = pagesize,
                tongtrang = totalPages,
                data = paginatedData
            };
        }
    }
}
