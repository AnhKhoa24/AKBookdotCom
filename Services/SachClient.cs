using AKBookdotCom.Contacts;
using AKBookdotCom.Models.Entities;
using AKBookdotCom.Models.Support;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace AKBookdotCom.Services
{
    public class SachClient : ISachClient
    {
        private readonly QuanLySachContext _context;

        public SachClient(QuanLySachContext context)
        {
            _context = context;
        }
        public async Task<List<Sach>> getSaches(PayloadIndex model)
        {
            string search = model.Search??"";
            int pagesize = 12;
            var data = await _context.Saches
                .Where(x=>(x.Tensach!.Contains(search)||search == "")
                && ((x.MaCd == model.MaCD || model.MaCD == 0) && (x.MaNxb == model.MaNXB || model.MaNXB == 0))
                )
                .Skip((model.Trang - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();
            return data;
        }
        public async Task<List<Chude>> getChuDe()
        {
            return await _context.Chudes.ToListAsync();
        }

        public async Task<List<Nhaxuatban>> getNhaxuatban()
        {
            return await _context.Nhaxuatbans.ToListAsync();
        }

        public async Task<List<string>> getSub(string search)
        {
            var tim = search.Trim().ToLower();
            var ds = await _context.Saches.Where(x=>x.Tensach!.Contains(tim))
                .Select(x => x.Tensach)
                .ToListAsync();
            return ds;
        }
    }
}
