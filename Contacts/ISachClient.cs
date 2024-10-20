using AKBookdotCom.Models.Entities;
using AKBookdotCom.Models.Support;

namespace AKBookdotCom.Contacts
{
    public interface ISachClient
    {
        Task<List<Chude>> getChuDe();
        Task<List<Nhaxuatban>> getNhaxuatban();
        Task<List<Sach>> getSaches(PayloadIndex model);
        Task<List<string>> getSub(string search);
    }
}
