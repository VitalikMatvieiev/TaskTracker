using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace TreloBLL.ClaimsPrincipalExtensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirstValue("UserId"));
        }
    }
}
