using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TreloBLL.DtoModel;
using TreloBLL.Interfaces;
using TreloDAL.Models;
using TreloDAL.UnitOfWork;

namespace TreloBLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ip)
        {
            User user = _unitOfWork.Users.FirstOrDefault(x => x.Email == model.Username && x.Password == model.Password, includeProperties: "Role,RefreshTokens");
            
            if(user == null)
            {
                return null;
            }

            var jwtToken = GenerateJWTToken(user);
            var refreshTokenDto = GenerateRefreshToken(ip);

            var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
            user.RefreshTokens.Add(refreshToken);
            _unitOfWork.SaveChanges();

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = _unitOfWork.Users.FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == token), includeProperties: "Role,RefreshTokens");

            if (user == null)
            {
                return null;
            }

            var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token);

            var newRefreshTokenDto = GenerateRefreshToken(ipAddress);

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshTokenDto.Token;

            var newRefreshToken = _mapper.Map<RefreshToken>(newRefreshTokenDto);

            user.RefreshTokens.Add(newRefreshToken);

            _unitOfWork.SaveChanges();

            var jwtToken = GenerateJWTToken(user);

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public bool RevokeToken(string token, string ipAddress)
        {
            var user = _unitOfWork.Users.FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == token), includeProperties: "RefreshTokens");

            if (user == null)
            {
                return false;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);


            if (!refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _unitOfWork.SaveChanges();

            return true;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            };
            foreach (var role in user.Role)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private string GenerateJWTToken(User user)
        {
            var identity = GetIdentity(user);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddMinutes(/*TimeSpan.FromMinutes(AuthOptions.LIFETIME)*/1),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private RefreshTokenDto GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshTokenDto
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }

    }
}
