namespace WebApiProductCRUD.Services.Security
{
    internal static class JwtConst
    {
        internal const string Bearer = "Bearer";

        internal static string Issuer => "Issuer";
        internal static string Audience => "audience-clients";
        internal static string CookieName => "jwtc";
        internal static string RefreshCookieName => "rftc";
        internal static string RefreshUsernameCookieName => "runc";
    }
}
