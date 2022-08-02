using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProductCRUD.Areas.Api.Controllers
{
    [Area("Api")]
    public class HomeController : ControllerBase
    {
        [AllowAnonymous]
        public string Content() => "Public Content";

        [Authorize]
        public string ContentPolicy() => "Content for Policy Auth";

        [Authorize]
        public IActionResult ContentPersonal()
            => Content("Content for Logged User: " + User?.Identity?.Name);
    }
}
