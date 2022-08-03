using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProductCRUD.Models.Security
{
    public static class TokenExpirationString
    {
        public const string Format = "yyyy-MM-dd HH:mm:ss";

        public static DateTime ToDateTime(string tokenExpirationString)
        {
            if (string.IsNullOrEmpty(tokenExpirationString))
            {
                return DateTime.Now.AddSeconds(-1);
            }
            if (DateTime.TryParseExact(tokenExpirationString, Format, new CultureInfo("pt-BR"),
                DateTimeStyles.None, out DateTime parsedValue))
            {
                return parsedValue;
            }
            return DateTime.Now.AddSeconds(-1);
        }
    }
}
