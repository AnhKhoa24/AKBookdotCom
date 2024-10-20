namespace AKBookdotCom.Areas.Admin.Contacts
{
    public interface IQuanLySach
    {
        Task<dynamic> SachService(int trang, int pagesize, string search);
        //Task<dynamic> getTacGia(int trang, int pagesize, string search);
        //Task<dynamic> getNhaSanXuat(int trang, int pagesize, string search);
        Task<dynamic> getDonHang(int trang, int pagesize, string search);
    }
}
