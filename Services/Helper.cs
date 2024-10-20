using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace AKBookdotCom.Services
{
    public static class Helper
    {
        public static string HashPassword(string password)
        {
            HashAlgorithm hashAlgorithm = SHA512.Create();
            return Convert.ToHexString(hashAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(password)));
        }
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }

}
