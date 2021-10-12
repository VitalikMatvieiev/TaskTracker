using System;
using System.Collections.Generic;
using System.Text;
using TreloDAL.Models;

namespace TreloBLL.DtoModel
{
    public class AuthenticateResponse
    {
        public string UserLogin { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            UserLogin = user.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
