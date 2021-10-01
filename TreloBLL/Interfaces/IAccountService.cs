using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace TreloBLL.Interfaces
{
    public interface IAccountService
    {
        ClaimsIdentity GetIdentity(string email, string password);
    }
}
