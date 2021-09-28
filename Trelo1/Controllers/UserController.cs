using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using Trelo1.Models;
using Trelo1.Models.ViewModel;

namespace Trelo1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly MapperConfiguration config;
        private readonly Mapper mapper;
        public UserController(IUserService userService)
        {
            _userService = userService;
            config = new MapperConfiguration(cnf =>
            {
                cnf.CreateMap<User, UserViewModel>();
            });
            mapper = new Mapper(config);
        }

        [HttpGet]
        public IList<UserViewModel> GetAllUsers()
        {
            IList<User> users = _userService.GetAllUsers();
            IList<UserViewModel> usersVM = mapper.Map<IList<UserViewModel>>(users);
            return usersVM;
        }
        [HttpPost]
        [ActionName("CreateUser")]
        public IActionResult CreateUser([FromBody]User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            _userService.Create(user);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }

    }
}
