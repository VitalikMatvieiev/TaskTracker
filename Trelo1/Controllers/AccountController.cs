using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using TreloBLL;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;

namespace Trelo1.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("api/account/authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest authenticateRequest)
        {
            var tokens = await _accountService.Authenticate(authenticateRequest, IpAddress());

            if (tokens == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            SetTokenCookie(tokens.RefreshToken);

            return Ok(tokens);
        }

        [HttpPost()]
        [Route("api/account/refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshToken(refreshToken, IpAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            SetTokenCookie(response.RefreshToken);

            return Ok(response);

        }

        [HttpPost()]
        [Route("api/account/revoke")]
        public async Task<IActionResult> RevokeToken([FromForm]string tokenForRevoke)
        {
            var token = tokenForRevoke ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = await _accountService.RevokeToken(token, IpAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
