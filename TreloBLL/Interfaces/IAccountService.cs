using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TreloBLL.DtoModel;

namespace TreloBLL.Interfaces
{
    public interface IAccountService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ip);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        bool RevokeToken(string token, string ipAddress);

    }
}
