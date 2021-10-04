using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
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
        public ClaimsIdentity GetIdentity(string email, string password)
        {
            User person = _unitOfWork.Users.FirstOrDefault(x => x.Email == email && x.Password == password, includeProperties: "Role");
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                };
                foreach (var role in person.Role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }

                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }


            return null;
        }
    }
}
