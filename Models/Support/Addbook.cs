namespace AKBookdotCom.Models.Support
{
    public class Addbook
    {
        public IFormFile? file { get; set; }
        public string? TenSach { get; set; }
        public long MaSach { get; set; }
        public string? MoTa { get; set; }
        public decimal? GiaBan { get; set; }
        public int SLTon { get; set; }
        public int MaNXB { get; set; }
        public int MaCD { get; set; }
        public List<int>? TacGias { get; set; }
        
    }
}
