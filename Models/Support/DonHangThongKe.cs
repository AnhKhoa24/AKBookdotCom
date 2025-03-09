using Microsoft.EntityFrameworkCore;

namespace AKBookdotCom.Models.Support
{
    [Keyless]
    public class DonHangThongKe
    {
        public DateTime Ngay { get; set; }
        public int SoLuongDon { get; set; }
    }
}
