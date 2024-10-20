
using AKBookdotCom.Models.Support;
using static AKBookdotCom.Models.Support.Response;

namespace AKBookdotCom.Contacts
{
    public interface IAuthService
    {
        Task<GeneralResponse> Register(Register model);
        Task<LoginResponse> Login(Login model);
        Task<GeneralResponse> Logout();
    }
}
