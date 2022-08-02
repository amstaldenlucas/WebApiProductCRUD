namespace WebApiProductCRUD.Models.Security
{
    public class AuthenticationResult
    {
        public bool Authenticated { get; set; }
        /// <summary>
        /// <see cref="AuthenticationResultMessageCode" />
        /// </summary>
        public string? MessageCode { get; set; }

        public string? DbUserEmail { get; set; }
        public string? DbUserName { get; set; }

        public Token? Token { get; set; }
    }
}
