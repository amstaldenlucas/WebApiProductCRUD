using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Models.Security;

namespace WebApiProductCRUD.Services.Security
{
    public interface ITokenService
    {
        Token GenerateToken(DbUser dbUser);
    }
}
