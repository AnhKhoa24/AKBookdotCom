namespace AKBookdotCom.Models.Support
{
    public class Response
    {
        public record class GeneralResponse(bool Flag, string Message);
        public record class LoginResponse(bool Flag, string? Role, string? Message);
        public record class ResponseOtp(bool Flag, string? Message);
    }
}
