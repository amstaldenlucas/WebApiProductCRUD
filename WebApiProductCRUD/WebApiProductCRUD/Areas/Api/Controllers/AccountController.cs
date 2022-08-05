using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Models.Security;
using WebApiProductCRUD.Services.Security;

namespace WebApiProductCRUD.Areas.Api.Controllers
{
    [Area("Api")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(AppDbContext dbContext, UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<object>> Token([FromBody] AccessCredential credential)
        {
            return await TryAuthenticateAndGenerateToken(credential);
        }

        private async Task<AuthenticationResult> TryAuthenticateAndGenerateToken(AccessCredential credential)
        {
            var dbUser = await _userManager.FindByEmailAsync(credential.Email);
            if (dbUser is null)
                return GetNoAccessObject(AuthenticationResultMessageCode.Error);

            var result = await _signInManager.CheckPasswordSignInAsync(dbUser, credential.Password, false);
            if (!result.Succeeded)
                return GetNoAccessObject(AuthenticationResultMessageCode.Unathorized);

            var token = _tokenService.GenerateToken(dbUser);
            return new AuthenticationResult
            {
                Authenticated = true,
                DbUserEmail = dbUser.Email,
                DbUserName = dbUser.Name,
                Token = token,
            };
        }

        private static AuthenticationResult GetNoAccessObject(string? messageCode = null)
        {
            return new AuthenticationResult
            {
                Authenticated = false,
                MessageCode = messageCode
            };
        }
    }
}
